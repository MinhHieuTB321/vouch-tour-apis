using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.Product;
using AutoMapper;
using Domain.Entities;
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
        public ProductService( IMapper mapper, IUnitOfWork unitOfWork)
        {
           
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Product> CreateProduct(CreateProductDTO createDTO)
        {   
                var createItem = _mapper.Map<Product>(createDTO);
            if (createItem != null)
            {
                await _unitOfWork.ProductRepository.AddAsync(createItem);
                if (await _unitOfWork.SaveChangeAsync() > 0) return createItem;
                else throw new Exception("Save changes failed");
            }
            else throw new Exception("Error Mapping");
            
                
        }

        public async Task<Product> GetById(Guid id)
        {
            var result = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (result is not null) return result;
            else throw new Exception("Not found");
        }
    }
}
