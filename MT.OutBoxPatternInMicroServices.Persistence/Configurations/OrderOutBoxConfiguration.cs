using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MT.OutBoxPatternInMicroServices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.OutBoxPatternInMicroServices.Persistence.Configurations
{
    public class OrderOutBoxConfiguration : IEntityTypeConfiguration<OrderOutBox>
    {
        public void Configure(EntityTypeBuilder<OrderOutBox> builder)
        {
            builder.HasKey(p => p.IdempotentToken);
        }
    }
}
