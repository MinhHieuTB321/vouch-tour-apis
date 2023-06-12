using Application.Interfaces;
using Application.ViewModels.Product;
using Application.ViewModels.SupplierDTO;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WApplication.GlobalExceptionHandling.Exceptions;

namespace Application.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimsService _claimsService;
        public SupplierService(IUnitOfWork unitOfWork, IMapper mapper,IClaimsService claimsService)
        {
             _mapper = mapper;
            _unitOfWork = unitOfWork;
            _claimsService = claimsService;
        }
        public async Task<SupplierViewDTO> Create(SupplierCreateDTO createdItem)
        {
            var createDTO = _mapper.Map<Supplier>(createdItem);
            createDTO.AdminId = _claimsService.GetCurrentUser;
            var supplier= await _unitOfWork.SupplierRepository.AddSupplierAsync(createDTO);
            var newUser = new User{UserId=supplier.Id,RoleId=2,Email=createdItem.Email}; 
            await _unitOfWork.UserRepository.AddAsync(newUser);
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<SupplierViewDTO>(supplier);
        }
        
        public async Task<bool> Delete(Guid id)
        {
           var supplier=await _unitOfWork.SupplierRepository.GetByIdAsync(id);
            if(supplier == null)
            {
                throw new NotFoundException("Supplier is not exist in system!");
            }
             _unitOfWork.SupplierRepository.SoftRemove(supplier);
            var result = await _unitOfWork.SaveChangeAsync();
            return result > 0;
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

        public async Task<bool> Update(SupplierUpdateDTO updatedItem)
        {
            var supplier = await _unitOfWork.SupplierRepository.GetByIdAsync(_claimsService.GetCurrentUser);
            if(supplier == null)
            {
                throw new NotFoundException("Suppler is not exist!");
            }
            supplier = _mapper.Map(updatedItem,supplier);
            _unitOfWork.SupplierRepository.Update(supplier);
            var result = await _unitOfWork.SaveChangeAsync();
            return result > 0;
            
        }
    }
}
