
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

            //Bir job tan�mlan�p 'OrderOutboxPublishJob' isimli s�n�fa ba�lan�yor.
            configurator.AddJob<OrderOutboxPublishJob>(options => options.WithIdentity(jobKey));

            TriggerKey triggerKey = new("OrderOutboxPublishTrigger");
            //Job 5 saniyelik aral�klarla �al��acak �ekilde ayarlan�yor.
            configurator.AddTrigger(options => options.ForJob(jobKey)
                        .WithIdentity(triggerKey)
                        .StartAt(DateTime.UtcNow)//Trigger'�n ba�lang�� tarihini belirliyoruz.
                        .WithSimpleSchedule//Trigger'�n ba�lad�ktan sonraki program�n� belirtiyoruz.
                        (
                            builder => builder.WithIntervalInSeconds(5) //Trigger'�n ka� saniyede bir tetiklenece�ini belirliyoruz.
                                              .RepeatForever() //Trigger'�n sonsuza denk �al��aca��n� belirtiyoruz.
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