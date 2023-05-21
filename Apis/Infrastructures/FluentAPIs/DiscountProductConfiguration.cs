using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class DiscountProductConfiguration : IEntityTypeConfiguration<DiscountProduct>
    {
         public void Configure(EntityTypeBuilder<DiscountProduct> builder)
        {
           builder.HasKey(x => x.Id);
           builder.HasOne(d => d.Product).WithMany(p => p.DiscountProducts)
           .HasForeignKey(d => d.ProductId)
           .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.Menu).WithMany(m => m.DiscountProducts)
            .HasForeignKey(d => d.MenuId)
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}