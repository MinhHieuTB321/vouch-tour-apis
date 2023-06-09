using Application.ViewModels.CategoryDTO;
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
    }
}
