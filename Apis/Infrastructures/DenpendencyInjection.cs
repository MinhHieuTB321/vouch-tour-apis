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


            services.AddScoped<IUnitOfWork, UnitOfWork>();

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
