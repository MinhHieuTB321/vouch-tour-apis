using Application;
using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.ChemicalsViewModels;
using AutoMapper;
using Domain.Entities;

namespace Infrastructures.Services
{
    public class ChemicalService : IChemicalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ChemicalService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ChemicalViewModel>> GetChemicalAsync()
        {
            var chemicals = await _unitOfWork.ChemicalRepository.GetAllAsync();
            var result = _mapper.Map<List<ChemicalViewModel>>(chemicals);
            return result;
        }

        public async Task<ChemicalViewModel?> CreateChemicalAsync(CreateChemicalViewModel chemical)
        {
            var chemicalObj = _mapper.Map<Chemical>(chemical);
            await _unitOfWork.ChemicalRepository.AddAsync(chemicalObj);
            var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
            if (isSuccess)
            {
                return _mapper.Map<ChemicalViewModel>(chemicalObj);
            }
            return null;
        }

        public async Task<Pagination<ChemicalViewModel>> GetChemicalPagingsionAsync(int pageIndex = 0, int pageSize = 10)
        {
            var chemicals = await _unitOfWork.ChemicalRepository.ToPagination(pageIndex, pageSize);
            var result = _mapper.Map<Pagination<ChemicalViewModel>>(chemicals);
            return result;
        }
    }
}
