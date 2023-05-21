using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {

            builder.HasKey(x => x.Id);
            builder.HasOne(g => g.TourGuide).WithMany(t => t.Groups).HasForeignKey(g => g.TourGuideId);
            
        }
    }
}