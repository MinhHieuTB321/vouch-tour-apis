using AutoFixture;
using FluentAssertions;
using Domain.Tests;
using Application.Commons;

namespace Application.Tests.Commons
{
    public class PaginationTests : SetupTest
    {

        [Fact]
        public void Pagination_PaginationFirstPage_ShouldReturnExpectedObject()
        {
            //arrange
            var mockItems = _fixture.Build<string>().CreateMany(100).ToList();
            //act

            var paginasion = new Pagination<string>
            {
                Items = mockItems,
                PageSize = 10,
                PageIndex = 0,
                TotalItemsCount = mockItems.Count,
            };

            //assert
            paginasion.Previous.Should().BeFalse();
            paginasion.Next.Should().BeTrue();
            paginasion.Items.Should().NotBeNullOrEmpty();
            paginasion.TotalItemsCount.Should().Be(100);
            paginasion.TotalPagesCount.Should().Be(10);
            paginasion.PageIndex.Should().Be(0);
            paginasion.PageSize.Should().Be(10);
        }

        [Fact]
        public void Pagination_PaginationSecoundPage_ShouldReturnExpectedObject()
        {
            //arrange
            var mockItems = _fixture.Build<string>().CreateMany(100).ToList();

            //act
            var paginasion = new Pagination<string>
            {
                Items = mockItems,
                PageSize = 10,
                PageIndex = 1,
                TotalItemsCount = mockItems.Count,
            };

            //assert
            paginasion.Previous.Should().BeTrue();
            paginasion.Next.Should().BeTrue();
            paginasion.Items.Should().NotBeNullOrEmpty();
            paginasion.TotalItemsCount.Should().Be(100);
            paginasion.TotalPagesCount.Should().Be(10);
            paginasion.PageIndex.Should().Be(1);
            paginasion.PageSize.Should().Be(10);
        }

        [Fact]
        public void Pagination_PaginationLastPage_ShouldReturnExpectedObject()
        {
            //arrange
            var mockItems = _fixture.Build<string>().CreateMany(101).ToList();

            //act
            var paginasion = new Pagination<string>
            {
                Items = mockItems,
                PageSize = 10,
                PageIndex = 10,
                TotalItemsCount = mockItems.Count,
            };

            //assert
            paginasion.Previous.Should().BeTrue();
            paginasion.Next.Should().BeFalse();
            paginasion.Items.Should().NotBeNullOrEmpty();
            paginasion.TotalItemsCount.Should().Be(101);
            paginasion.TotalPagesCount.Should().Be(11);
            paginasion.PageIndex.Should().Be(10);
            paginasion.PageSize.Should().Be(10);
        }
    }
}
