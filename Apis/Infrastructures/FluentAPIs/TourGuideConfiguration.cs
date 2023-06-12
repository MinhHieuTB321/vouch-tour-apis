using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.FluentAPIs
{
    public class TourGuideConfiguration : IEntityTypeConfiguration<TourGuide>
    {
        public void Configure(EntityTypeBuilder<TourGuide> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(t => t.Admin)
                .WithMany(a => a.TourGuides)
                .HasForeignKey(t => t.AdminId);
            builder.Property(t => t.Status).HasDefaultValue(ActiveEnum.Active.ToString());
        }
    }
}
