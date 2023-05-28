using Application;
using Application.Interfaces;
using Application.Repositories;
using Application.Services;
using Infrastructures.Mappers;
using Infrastructures.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructures
{
    public static class DenpendencyInjection
    {
        public static IServiceCollection AddInfrastructuresService(this IServiceCollection services, string DbConnection)
        {
            #region DI_SERVICES
            services.AddScoped<IUserService, UserService>();
            #endregion

            #region DI_REPOSITORY
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion
            services.AddSingleton<ICurrentTime, CurrentTime>();
            services.AddDbContext<AppDbContext>(
                option => option.UseSqlServer(DbConnection));
            // services.AddDbContext<AppDbContext>(
            //     option => option.UseInMemoryDatabase("test"));
            services.AddAutoMapper(typeof(MapperConfigurationsProfile).Assembly);
            return services;
        }
    }
}
