using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
         public void Configure(EntityTypeBuilder<Payment> builder)
        {
           builder.HasKey(x => x.Id);
           
           builder.HasOne(p => p.Order).WithMany(o => o.Payments)
           .HasForeignKey(p => p.OrderId);
           
        }
    }
}