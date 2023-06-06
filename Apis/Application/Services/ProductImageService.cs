using Application.Commons;
using Application.Interfaces;
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
        public ProductImageService(IUnitOfWork unitOfWork)
        {
                _unitOfWork = unitOfWork;
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
    }
}
