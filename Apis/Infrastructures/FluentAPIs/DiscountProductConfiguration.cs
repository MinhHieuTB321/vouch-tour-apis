using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class DiscountProductConfiguration : IEntityTypeConfiguration<ProductInMenu>
    {
         public void Configure(EntityTypeBuilder<ProductInMenu> builder)
        {
           builder.HasKey(x => x.Id);
           builder.HasOne(d => d.Product).WithMany(p => p.ProductInMenus)
           .HasForeignKey(d => d.ProductId)
           .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.Menu).WithMany(m => m.ProductInMenus)
            .HasForeignKey(d => d.MenuId)
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}