using Application.Repositories;
using AutoFixture;
using Domain.Entities;
using Domain.Tests;
using FluentAssertions;
using Infrastructures.Repositories;

namespace Infrastructures.Tests.Repositories
{
    public class GenericRepositoryTests : SetupTest
    {
        private readonly IGenericRepository<Chemical> _genericRepository;
        public GenericRepositoryTests()
        {
            _genericRepository = new GenericRepository<Chemical>(
                _dbContext,
                _currentTimeMock.Object,
                _claimsServiceMock.Object);
        }

        [Fact]
        public async Task GenericRepository_GetAllAsync_ShouldReturnCorrectData()
        {
            var mockData = _fixture.Build<Chemical>().CreateMany(10).ToList();
            await _dbContext.Chemicals.AddRangeAsync(mockData);

            await _dbContext.SaveChangesAsync();


            var result = await _genericRepository.GetAllAsync();

            result.Should().BeEquivalentTo(mockData);
        }


        [Fact]
        public async Task GenericRepository_GetAllAsync_ShouldReturnEmptyWhenHaveNoData()
        {

            var result = await _genericRepository.GetAllAsync();

            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GenericRepository_GetByIdAsync_ShouldReturnCorrectData()
        {
            var mockData = _fixture.Build<Chemical>().Create();
            await _dbContext.Chemicals.AddRangeAsync(mockData);

            await _dbContext.SaveChangesAsync();


            var result = await _genericRepository.GetByIdAsync(mockData.Id);

            result.Should().BeEquivalentTo(mockData);
        }


        [Fact]
        public async Task GenericRepository_GetByIdAsync_ShouldReturnEmptyWhenHaveNoData()
        {

            var result = await _genericRepository.GetByIdAsync(Guid.Empty);

            result.Should().BeNull();
        }

        [Fact]
        public async Task GenericRepository_AddAsync_ShouldReturnCorrectData()
        {
            var mockData = _fixture.Build<Chemical>().Create();


            await _genericRepository.AddAsync(mockData);
            var result = await _dbContext.SaveChangesAsync();

            result.Should().Be(1);
        }

        [Fact]
        public async Task GenericRepository_AddRangeAsync_ShouldReturnCorrectData()
        {
            var mockData = _fixture.Build<Chemical>().CreateMany(10).ToList();


            await _genericRepository.AddRangeAsync(mockData);
            var result = await _dbContext.SaveChangesAsync();

            result.Should().Be(10);
        }


        [Fact]
        public async Task GenericRepository_SoftRemove_ShouldReturnCorrectData()
        {
            var mockData = _fixture.Build<Chemical>().Create();
            _dbContext.Chemicals.Add(mockData);
            await _dbContext.SaveChangesAsync();


            _genericRepository.SoftRemove(mockData);
            var result = await _dbContext.SaveChangesAsync();

            result.Should().Be(1);
        }

        [Fact]
        public async Task GenericRepository_Update_ShouldReturnCorrectData()
        {
            var mockData = _fixture.Build<Chemical>().Create();
            _dbContext.Chemicals.Add(mockData);
            await _dbContext.SaveChangesAsync();


            _genericRepository.Update(mockData);
            var result = await _dbContext.SaveChangesAsync();

            result.Should().Be(1);
        }

        [Fact]
        public async Task GenericRepository_SoftRemoveRange_ShouldReturnCorrectData()
        {
            var mockData = _fixture.Build<Chemical>().CreateMany(10).ToList();
            await _dbContext.Chemicals.AddRangeAsync(mockData);
            await _dbContext.SaveChangesAsync();


            _genericRepository.SoftRemoveRange(mockData);
            var result = await _dbContext.SaveChangesAsync();

            result.Should().Be(10);
        }

        [Fact]
        public async Task GenericRepository_UpdateRange_ShouldReturnCorrectData()
        {
            var mockData = _fixture.Build<Chemical>().CreateMany(10).ToList();
            await _dbContext.Chemicals.AddRangeAsync(mockData);
            await _dbContext.SaveChangesAsync();


            _genericRepository.UpdateRange(mockData);
            var result = await _dbContext.SaveChangesAsync();

            result.Should().Be(10);
        }

        [Fact]
        public async Task GenericRepository_ToPagination_ShouldReturnCorrectDataFirstsPage()
        {
            var mockData = _fixture.Build<Chemical>().CreateMany(45).ToList();
            await _dbContext.Chemicals.AddRangeAsync(mockData);
            await _dbContext.SaveChangesAsync();


            var paginasion = await _genericRepository.ToPagination();


            paginasion.Previous.Should().BeFalse();
            paginasion.Next.Should().BeTrue();
            paginasion.Items.Count.Should().Be(10);
            paginasion.TotalItemsCount.Should().Be(45);
            paginasion.TotalPagesCount.Should().Be(5);
            paginasion.PageIndex.Should().Be(0);
            paginasion.PageSize.Should().Be(10);
        }

        [Fact]
        public async Task GenericRepository_ToPagination_ShouldReturnCorrectDataSecoundPage()
        {
            var mockData = _fixture.Build<Chemical>().CreateMany(45).ToList();
            await _dbContext.Chemicals.AddRangeAsync(mockData);
            await _dbContext.SaveChangesAsync();


            var paginasion = await _genericRepository.ToPagination(1, 20);


            paginasion.Previous.Should().BeTrue();
            paginasion.Next.Should().BeTrue();
            paginasion.Items.Count.Should().Be(20);
            paginasion.TotalItemsCount.Should().Be(45);
            paginasion.TotalPagesCount.Should().Be(3);
            paginasion.PageIndex.Should().Be(1);
            paginasion.PageSize.Should().Be(20);
        }

        [Fact]
        public async Task GenericRepository_ToPagination_ShouldReturnCorrectDataLastPage()
        {
            var mockData = _fixture.Build<Chemical>().CreateMany(45).ToList();
            await _dbContext.Chemicals.AddRangeAsync(mockData);
            await _dbContext.SaveChangesAsync();


            var paginasion = await _genericRepository.ToPagination(2, 20);


            paginasion.Previous.Should().BeTrue();
            paginasion.Next.Should().BeFalse();
            paginasion.Items.Count.Should().Be(5);
            paginasion.TotalItemsCount.Should().Be(45);
            paginasion.TotalPagesCount.Should().Be(3);
            paginasion.PageIndex.Should().Be(2);
            paginasion.PageSize.Should().Be(20);
        }

        [Fact]
        public async Task GenericRepository_ToPagination_ShouldReturnWithoutData()
        {
            var paginasion = await _genericRepository.ToPagination();


            paginasion.Previous.Should().BeFalse();
            paginasion.Next.Should().BeFalse();
            paginasion.Items.Count.Should().Be(0);
            paginasion.TotalItemsCount.Should().Be(0);
            paginasion.TotalPagesCount.Should().Be(0);
            paginasion.PageIndex.Should().Be(0);
            paginasion.PageSize.Should().Be(10);
        }
    }
}
