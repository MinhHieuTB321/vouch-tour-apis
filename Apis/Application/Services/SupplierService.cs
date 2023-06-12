using Application.Interfaces;
using Application.ViewModels.SupplierDTO;
using AutoMapper;
using Domain.Entities;
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
            createDTO.AdminId = _claimsService.GetUserRoleId;
            var supplier= await _unitOfWork.SupplierRepository.AddSupplierAsync(createDTO);
            var user = _mapper.Map<User>(createdItem);
            user.UserId = supplier.Id;
            user.RoleId = 2;
            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<SupplierViewDTO>(supplier);
        }


        //private Task CreateUser(User user)
        //{

        //}
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
