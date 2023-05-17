using Application.Interfaces;
using Application.ViewModels.ChemicalsViewModels;
using Application.Commons;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class ChemicalController : BaseController
    {
        private readonly IChemicalService _chemicalService;

        public ChemicalController(IChemicalService chemicalService)
        {
            _chemicalService = chemicalService;
        }

        [HttpGet]
        public async Task<Pagination<ChemicalViewModel>> GetChemicalPagingsion(int pageIndex = 0, int pageSize = 10)
        {
            return await _chemicalService.GetChemicalPagingsionAsync(pageIndex, pageSize);
        }

        [HttpPost]
        public async Task<ChemicalViewModel?> CreateChemical(CreateChemicalViewModel chemical)
        {
            return await _chemicalService.CreateChemicalAsync(chemical);
        }
    }
}