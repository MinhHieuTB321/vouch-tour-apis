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
        Task<IEnumerable<ViewProductDTO>> GetAll();
        Task<ViewProductDTO> GetById(Guid id);
        Task<bool> UpdateProduct(UpdateProductDTO updateDTO);
        Task<bool> DeleteProduct(Guid id);

        
    }
}
