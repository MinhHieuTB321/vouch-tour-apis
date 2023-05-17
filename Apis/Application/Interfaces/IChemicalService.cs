using Application.Commons;
using Application.ViewModels.ChemicalsViewModels;

namespace Application.Interfaces
{
    public interface IChemicalService
    {
        public Task<List<ChemicalViewModel>> GetChemicalAsync();
        public Task<ChemicalViewModel?> CreateChemicalAsync(CreateChemicalViewModel chemical);
        public Task<Pagination<ChemicalViewModel>> GetChemicalPagingsionAsync(int pageIndex = 0, int pageSize = 10);
    }
}
