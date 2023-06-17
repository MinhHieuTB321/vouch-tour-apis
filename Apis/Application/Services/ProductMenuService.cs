using Application.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.GlobalExceptionHandling.Exceptions;

namespace Application.Services
{
    public class ProductMenuService : IProductMenuService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsService _claimsService;
        private readonly IMapper _mapper;

        public ProductMenuService(IUnitOfWork unitOfWork, IClaimsService claimsService, IMapper mapper)
        {
            _claimsService = claimsService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> DeleteProductFromMenu(Guid productMenuId)
        {
            var deleteDTO = await _unitOfWork.ProductMenuRepository.FindByField(x=>x.ProductId == productMenuId);
            if (deleteDTO == null) { throw new NotFoundException("Not Found!"); }
            deleteDTO.IsDeleted = true;
            _unitOfWork.ProductMenuRepository.Update(deleteDTO);
            var result = await _unitOfWork.SaveChangeAsync();
            return result > 0;
        }
    }
}
