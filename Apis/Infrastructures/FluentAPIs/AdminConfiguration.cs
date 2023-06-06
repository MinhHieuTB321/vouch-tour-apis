using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.FluentAPIs
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasData(new Admin
            {
                PhoneNumber = "0909014406",
                Email = "quangtm0152@gmail.com",
               DateOfBirth = DateTime.Now,
               Name = "QuangDepTry",
               Sex = 1,
               Status = "Active"
            });

        }
    }
}
