using Application.Interfaces;
using Application.Repositories;
using Application.Services;
using FluentAssertions;
using Infrastructures;
using Infrastructures.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using WebAPI.Middlewares;
using WebAPI.Services;

namespace WebAPI.Tests
{
    public class DependencyInjectionTests
    {
        private readonly ServiceProvider _serviceProvider;
        public DependencyInjectionTests()
        {
            var service = new ServiceCollection();
            service.AddWebAPIService("");
            service.AddDbContext<AppDbContext>(
                option => option.UseInMemoryDatabase("test"));
            _serviceProvider = service.BuildServiceProvider();
        }

        [Fact]
        public void DependencyInjectionTests_ServiceShouldResolveCorrectly()
        {
            // var currentTimeServiceResolved = _serviceProvider.GetRequiredService<ICurrentTime>();
            // var claimsServiceServiceResolved = _serviceProvider.GetRequiredService<IClaimsService>();
            // var exceptionMiddlewareResolved = _serviceProvider.GetRequiredService<ExceptionMiddleware>();
            // var performanceMiddleware = _serviceProvider.GetRequiredService<PerformanceMiddleware>();
            // var stopwatchResolved = _serviceProvider.GetRequiredService<Stopwatch>();


            // currentTimeServiceResolved.GetType().Should().Be(typeof(CurrentTime));
            // claimsServiceServiceResolved.GetType().Should().Be(typeof(ClaimsService));
            // exceptionMiddlewareResolved.GetType().Should().Be(typeof(ExceptionMiddleware));
            // performanceMiddleware.GetType().Should().Be(typeof(PerformanceMiddleware));
            // stopwatchResolved.GetType().Should().Be(typeof(Stopwatch));

        }
    }
}
