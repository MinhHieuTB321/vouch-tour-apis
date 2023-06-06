using Application.ViewModels.Product;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<Product> CreateProduct(CreateProductDTO createDTO);
        Task<Product> GetById(Guid id);

        
    }
}
