using Application;
using Application.Interfaces;
using Application.Repositories;
using Application.Services;
using Infrastructures.Mappers;
using Infrastructures.Repositories;
using Infrastructures.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructures
{
    public static class DenpendencyInjection
    {
        public static IServiceCollection AddInfrastructuresService(this IServiceCollection services)
        {
            services.AddScoped<IChemicalService, ChemicalService>();
            services.AddScoped<IChemicalRepository, ChemicalRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<ICurrentTime, CurrentTime>();
            //services.AddDbContext<AppDbContext>(
            //    option => option.UseSqlServer("Data Source=.;Initial Catalog=testdb3;Integrated Security=True;Trust Server Certificate=true"));
            services.AddDbContext<AppDbContext>(
                option => option.UseInMemoryDatabase("test"));
            services.AddAutoMapper(typeof(MapperConfigurationsProfile).Assembly);
            return services;
        }
    }
}
