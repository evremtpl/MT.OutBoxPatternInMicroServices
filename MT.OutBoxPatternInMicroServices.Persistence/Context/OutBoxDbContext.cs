using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MT.OutBoxPatternInMicroServices.Domain.Entities;
using MT.OutBoxPatternInMicroServices.Persistence.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.OutBoxPatternInMicroServices.Persistence.Context
{
    public class OutBoxDbContext :DbContext

    {
        private readonly IConfiguration _configuration;
        public OutBoxDbContext(IConfiguration configuration)
        {
            _configuration=configuration;
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderOutBox> OrderOutBoxes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder )
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OutBoxDbContext).Assembly);



            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderOutBoxConfiguration());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                _configuration.GetConnectionString("SqlSrvConStr"));
            }
        }
    }
}
