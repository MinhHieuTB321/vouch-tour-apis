using Application.Interfaces;
using Application.ViewModels.SupplierDTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SupplierService(IUnitOfWork unitOfWork, IMapper mapper)
        {
                _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public Task<SupplierViewDTO> Create(SupplierCreateDTO createdItem)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SupplierViewDTO>> GetAll()
        {
            var result = await _unitOfWork.SupplierRepository.GetAllAsync();
            if (result.Count > 0) return _mapper.Map<IEnumerable<SupplierViewDTO>>(result);
            else throw new Exception("Not have any supplier");
        }

        public async Task<SupplierViewDTO> GetById(Guid id)
        {
            var result = await _unitOfWork.SupplierRepository.GetByIdAsync(id);
            if (result != null) return _mapper.Map<SupplierViewDTO>(result);
            else throw new Exception("Not found");
        }

        public Task<bool> Update(SupplierUpdateDTO updatedItem)
        {
            throw new NotImplementedException();
        }
    }
}
