using Application.ViewModels.ChemicalsViewModels;
using AutoFixture;
using Domain.Entities;
using Domain.Tests;
using FluentAssertions;

namespace Infrastructures.Tests.Mappers
{
    public class MapperConfigurasionTests : SetupTest
    {
        [Fact]
        public void TestMapper()
        {
            //arrange
            var chemicalMock = _fixture.Build<Chemical>().Create();

            //act
            var result = _mapperConfig.Map<ChemicalViewModel>(chemicalMock);

            //assert
            result._Id.Should().Be(chemicalMock.Id.ToString());
        }
    }
}
