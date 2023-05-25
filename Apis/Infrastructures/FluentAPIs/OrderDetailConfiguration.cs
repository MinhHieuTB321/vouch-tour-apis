using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
          public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
           builder.HasKey(x => x.Id);
           builder.HasOne(o => o.Order).WithMany(o => o.OrderDetails)
           .HasForeignKey(o => o.OrderId);
           builder.HasOne(od => od.DiscountProduct).WithMany(d => d.OrderDetails)
           .HasForeignKey(od => od.DiscountProductId);



        }
    }
}