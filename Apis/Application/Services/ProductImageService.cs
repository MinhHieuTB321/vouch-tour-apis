using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.Product.ProductImage;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductImageService : IProductImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductImageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
        }
        public async Task<ProductImage?> AddImageAsync(IFormFile file, Guid productId)
        {
            var fireBaseFile = await file.UploadFileAsync();
            if(fireBaseFile is not null) 
            {
                var productImage = new ProductImage()
                {
                    FileName = fireBaseFile.FileName,
                    FileURL = fireBaseFile.URL,
                    ProductId = productId
                };
                await _unitOfWork.ProductImageRepository.AddAsync(productImage);
                if (await _unitOfWork.SaveChangeAsync() > 0) return (await _unitOfWork.ProductImageRepository.GetAllAsync()).Where(x => x.FileName == fireBaseFile.FileName).First();
                
            } return null;
        }

        public async Task<bool> DeleteImage(Guid id)
        {
            var deletedItem =  await _unitOfWork.ProductImageRepository.GetByIdAsync(id);
            if (deletedItem != null)
            {
                _unitOfWork.ProductImageRepository.SoftRemove(deletedItem);
                var result = await deletedItem.FileName.RemoveFileAsync();
                if (result) return true;
                else throw new Exception("Remove File at Firebase occured");
            }
            else throw new Exception("Not found");
        }

        public async Task<IEnumerable<ProductImageViewDTO>> GetAll()
        {
            var result = await _unitOfWork.ProductImageRepository.GetAllAsync();
            if (result.Count() > 0) return _mapper.Map<IEnumerable<ProductImageViewDTO>>(result);
            else throw new Exception();
        }

        public async Task<ProductImageViewDTO> GetById(Guid id)
        {
            var result = await _unitOfWork.ProductImageRepository.GetByIdAsync(id);
            if (result is not null)
            {
                return _mapper.Map<ProductImageViewDTO>(result);
            }
            else throw new Exception("Not found");
        }
    }
}
