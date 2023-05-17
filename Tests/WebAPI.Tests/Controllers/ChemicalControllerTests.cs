using Application.ViewModels.ChemicalsViewModels;
using AutoFixture;
using Application.Commons;
using Domain.Tests;
using FluentAssertions;
using Moq;
using WebAPI.Controllers;

namespace WebAPI.Tests.Controllers
{
    public class ChemicalControllerTests : SetupTest
    {
        private readonly ChemicalController _chemicalController;
        public ChemicalControllerTests()
        {
            _chemicalController = new ChemicalController(_chemicalServiceMock.Object);
        }

        [Fact]
        public async Task GetChemicalPagingsion_ShouldReturnCorrectDataWithDefaultParametor()
        {
            var mocks = _fixture.Build<Pagination<ChemicalViewModel>>().Create();
            // arrange
            _chemicalServiceMock.Setup(
                x => x.GetChemicalPagingsionAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(mocks);

            // act
            var result = await _chemicalController.GetChemicalPagingsion();

            // assert
            _chemicalServiceMock.Verify(
                x => x.GetChemicalPagingsionAsync(
                    It.Is<int>(x => x.Equals(0)),
                    It.Is<int>(x => x.Equals(10))), Times.Once());

            result.Should().BeEquivalentTo(mocks);
        }

        [Fact]
        public async Task GetChemicalPagingsion_ShouldReturnCorrectDataWithParametor()
        {
            var mocks = _fixture.Build<Pagination<ChemicalViewModel>>().Create();
            // arrange
            _chemicalServiceMock.Setup(
                x => x.GetChemicalPagingsionAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(mocks);

            // act
            var result = await _chemicalController.GetChemicalPagingsion(1, 100);

            // assert
            _chemicalServiceMock.Verify(
                x => x.GetChemicalPagingsionAsync(
                    It.Is<int>(x => x.Equals(1)),
                    It.Is<int>(x => x.Equals(100))), Times.Once());

            result.Should().BeEquivalentTo(mocks);
        }

        [Fact]
        public async Task CreateChemical_ShouldReturnCorrectData()
        {
            var mockModelRequest = _fixture.Build<CreateChemicalViewModel>().Create();
            var mockModelResponse = _fixture.Build<ChemicalViewModel>().Create();
            // arrange
            _chemicalServiceMock.Setup(
                x => x.CreateChemicalAsync(It.IsAny<CreateChemicalViewModel>()))
                        .ReturnsAsync(mockModelResponse);

            // act
            var result = await _chemicalController.CreateChemical(mockModelRequest);

            // assert
            _chemicalServiceMock.Verify(
                x => x.CreateChemicalAsync(It.Is<CreateChemicalViewModel>(
                    x => x.Equals(mockModelRequest))), Times.Once());

            result.Should().BeEquivalentTo(mockModelResponse);
        }
    }
}
