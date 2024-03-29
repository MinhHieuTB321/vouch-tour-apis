﻿using Application;
using Application.Interfaces;
using Application.Repositories;
using Application.Services;
using Infrastructures.Mappers;
using Infrastructures.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace Infrastructures
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructuresService(this IServiceCollection services, string DbConnection)
        {
            #region DI_SERVICES
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductImageService, ProductImageService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<ITourGuideService, TourGuideService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IProductMenuService, ProductMenuService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();
            #endregion

            #region DI_REPOSITORY
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            services.AddScoped<ITourGuideRepository, TourGuideRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IProductMenuRepository,ProductMenuRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            #endregion
            services.AddSingleton<ICurrentTime, CurrentTime>();
            services.AddDbContext<AppDbContext>(
                option => option.UseSqlServer(DbConnection));
            // services.AddDbContext<AppDbContext>(
            //     option => option.UseInMemoryDatabase("test"));
            services.AddAutoMapper(typeof(MapperConfigurationsProfile).Assembly);
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            return services;
        }
    }
}
