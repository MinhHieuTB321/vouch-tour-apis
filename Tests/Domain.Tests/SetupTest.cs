using Application;
using Application.Interfaces;
using Application.Repositories;
using AutoFixture;
using AutoMapper;
using Infrastructures;
using Infrastructures.Mappers;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Domain.Tests
{
    public class SetupTest : IDisposable
    {
        protected readonly IMapper _mapperConfig;
        protected readonly Fixture _fixture;
        protected readonly Mock<IUnitOfWork> _unitOfWorkMock;

        protected readonly Mock<IClaimsService> _claimsServiceMock;
        protected readonly Mock<ICurrentTime> _currentTimeMock;


        protected readonly AppDbContext _dbContext;

        public SetupTest()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperConfigurationsProfile());
            });
            _mapperConfig = mappingConfig.CreateMapper();
            _fixture = new Fixture();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _claimsServiceMock = new Mock<IClaimsService>();
            _currentTimeMock = new Mock<ICurrentTime>();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _dbContext = new AppDbContext(options);

            _currentTimeMock.Setup(x => x.GetCurrentTime()).Returns(DateTime.UtcNow);
            _claimsServiceMock.Setup(x => x.GetCurrentUserId).Returns(Guid.Empty);

        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
