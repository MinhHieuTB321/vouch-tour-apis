﻿using Application.ViewModels.Product.ProductImage;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductImageService
    {
        public Task<ProductImage?> AddImageAsync(IFormFile file, Guid productId);


        public Task<bool> DeleteImage(Guid id);
        public Task<IEnumerable<ProductImageViewDTO>> GetAll();
        public Task<ProductImageViewDTO> GetById(Guid id);
    }
}
