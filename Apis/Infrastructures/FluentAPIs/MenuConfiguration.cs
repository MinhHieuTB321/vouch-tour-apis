using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
          public void Configure(EntityTypeBuilder<Menu> builder)
        {
           builder.HasKey(x => x.Id);
           
           builder.HasOne(m => m.TourGuide)
           .WithMany(t => t.Menus).HasForeignKey(m => m.TourGuideId);
          builder.HasOne(x=>x.Group).WithMany(x=>x.Menus).HasForeignKey(x=>x.GroupId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}