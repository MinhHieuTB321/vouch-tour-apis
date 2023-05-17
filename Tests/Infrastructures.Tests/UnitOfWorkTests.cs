using Application;
using AutoFixture;
using Domain.Entities;
using Domain.Tests;
using FluentAssertions;
using Moq;

namespace Infrastructures.Tests
{
    public class UnitOfWorkTests : SetupTest
    {
        private readonly IUnitOfWork _unitOfWork;
        public UnitOfWorkTests()
        {
            _unitOfWork = new UnitOfWork(
                _dbContext,
                _chemicalRepositoryMock.Object,
                _userRepository.Object
                );
        }

        [Fact]
        public async Task TestUnitOfWork()
        {
            // arrange
            var mockData = _fixture.Build<Chemical>().CreateMany(10).ToList();

            _chemicalRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(mockData);

            // act
            var items = await _unitOfWork.ChemicalRepository.GetAllAsync();

            // assert
            items.Should().BeEquivalentTo(mockData);
        }

    }
}
