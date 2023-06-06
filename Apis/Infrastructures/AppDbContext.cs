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
        public DbSet<User> User {get;set;} = default!;
        public DbSet<Product> Product {get;set;} = default!;
        public DbSet<Group> Group {get;set;} = default!;
        public DbSet<ProductInMenu> ProductInMenu {get;set;} = default!;
        public DbSet<Menu> Menu {get;set;} = default!;
        public DbSet<Order> Order {get;set;} = default!;
        public DbSet<OrderDetail> OrderDetail {get;set;} = default!;
        public DbSet<Role> Role {get;set;} = default!;
        public DbSet<Category> Category {get;set;} = default!;
        public DbSet<TourGuide> TourGuide { get; set; } = default!;
        public DbSet<Supplier> Supplier { get; set; } = default!;
        public DbSet<Admin> Admin { get; set;} = default!;
        public DbSet<Payment> Payment {get;set;} = default!;
        public DbSet<ProductImage> ProductImage { get; set;} = default!;
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
