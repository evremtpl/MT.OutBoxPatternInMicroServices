
using Quartz;
using MassTransit;
using MT.OutBoxPatternInMicroServices.OutboxJob.Service;
using MT.OutBoxPatternInMicroServices.OutboxJob.Service.Jobs;
using System;
using Microsoft.Extensions.Hosting;
using MT.OutBoxPatternInMicroServices.Shared.RabbitMq;

Microsoft.Extensions.Hosting.IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddQuartz(configurator =>
        {
            configurator.UseMicrosoftDependencyInjectionJobFactory();

            JobKey jobKey = new("OrderOutboxPublishJob");

            //Bir job tanýmlanýp 'OrderOutboxPublishJob' isimli sýnýfa baðlanýyor.
            configurator.AddJob<OrderOutboxPublishJob>(options => options.WithIdentity(jobKey));

            TriggerKey triggerKey = new("OrderOutboxPublishTrigger");
            //Job 5 saniyelik aralýklarla çalýþacak þekilde ayarlanýyor.
            configurator.AddTrigger(options => options.ForJob(jobKey)
                        .WithIdentity(triggerKey)
                        .StartAt(DateTime.UtcNow)//Trigger'ýn baþlangýç tarihini belirliyoruz.
                        .WithSimpleSchedule//Trigger'ýn baþladýktan sonraki programýný belirtiyoruz.
                        (
                            builder => builder.WithIntervalInSeconds(5) //Trigger'ýn kaç saniyede bir tetikleneceðini belirliyoruz.
                                              .RepeatForever() //Trigger'ýn sonsuza denk çalýþacaðýný belirtiyoruz.
                        ));
        });

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        services.AddMassTransit(cfg =>
        {
            cfg.AddBus(provider => RabbitMqBus.ConfigureBus(provider));
        });
    })
    .Build();

await host.RunAsync();