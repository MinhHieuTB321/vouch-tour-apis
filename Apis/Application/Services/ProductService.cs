using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.Product;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimsService _claimsService;
        public ProductService( IMapper mapper, IUnitOfWork unitOfWork,IClaimsService claimsService)
        {
           
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _claimsService = claimsService;
        }
        public async Task<Product> CreateProduct(CreateProductDTO createDTO)
        {   
            var createItem = _mapper.Map<Product>(createDTO);
            createItem.SupplierId = _claimsService.GetCurrentUser;
            if (createItem != null)
            {
                await _unitOfWork.ProductRepository.AddAsync(createItem);
                if (await _unitOfWork.SaveChangeAsync() > 0) return createItem;
                else throw new Exception("Save changes failed");
            }
            else throw new Exception("Error Mapping");
            
                
        }

        public async Task<bool> DeleteProduct(Guid id)
        {
            var deletedItem = await _unitOfWork.ProductRepository.GetByIdAsync(id, x => x.Images);
            if (deletedItem != null)
            {
                _unitOfWork.ProductRepository.SoftRemove(deletedItem);
                if (await _unitOfWork.SaveChangeAsync() > 0) return true;
                else throw new Exception("Save change failed!");

            }
            else throw new Exception("Not found");
        }

        public async Task<IEnumerable<ViewProductDTO>> GetAll()
        {
            var result =  await _unitOfWork.ProductRepository.GetAllAsync(x => x.Images, x => x.Category, x=> x.Supplier);
            if (result.Count > 0) return _mapper.Map<IEnumerable<ViewProductDTO>>(result);
            else throw new Exception("Not have any product");


        }

        public async Task<ViewProductDTO> GetById(Guid id)
        {
            var result = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (result is not null) return _mapper.Map<ViewProductDTO>(result);
            else throw new Exception("Not found");
        }

        public async Task<bool> UpdateProduct(UpdateProductDTO updateDTO)
        {
            var updatedItem = await _unitOfWork.ProductRepository.GetByIdAsync(updateDTO.ProductId);
            if (updatedItem != null)
            {
                updatedItem = (Product)_mapper.Map(updateDTO, typeof(UpdateProductDTO), typeof(Product));
                _unitOfWork.ProductRepository.Update(updatedItem);
                if (await _unitOfWork.SaveChangeAsync() > 0) return true;
                else throw new Exception("Save change failed!");
            }
            else throw new Exception("Not found");
        }
    }
}
