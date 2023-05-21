using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
          public void Configure(EntityTypeBuilder<Product> builder)
        {
           builder.HasKey(x => x.Id);
           builder.HasOne(x => x.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey(x => x.CategoryId);
            builder.HasOne(x => x.Supplier)
            .WithMany(s => s.Products)
            .HasForeignKey(x => x.SupplierId);
        }
    }
}