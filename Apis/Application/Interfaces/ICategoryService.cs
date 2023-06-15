using Application.ViewModels.CategoryDTO;
using Application.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewDTO>> GetAll();
        Task<CategoryViewDTO> GetById(Guid Id);
        Task<CategoryViewDTO> Create(CategoryCreateDTO categoryCreateDTO);
        Task<bool> DeleteById(Guid Id);
        Task<bool> Update(CategoryUpdateDTO categoryUpdateDTO);

        Task<List<ViewProductDTO>> GetProuductByCategoryId(Guid Id);
    }
}
