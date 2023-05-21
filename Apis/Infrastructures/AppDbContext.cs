using Domain.Entities;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        #region DbSet
        public DbSet<User> Users {get;set;} = default!;
        public DbSet<Product> Products {get;set;} = default!;
        public DbSet<Group> Groups {get;set;} = default!;
        public DbSet<DiscountProduct> DiscountProducts {get;set;} = default!;
        public DbSet<Menu> Menus{get;set;} = default!;
        public DbSet<Order> Orders {get;set;} = default!;
        public DbSet<OrderDetail> OrderDetails {get;set;} = default!;
        public DbSet<Role> Roles {get;set;} = default!;
        public DbSet<Category> Categories {get;set;} = default!;

        public DbSet<Payment> Payments {get;set;} = default!;
        
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
