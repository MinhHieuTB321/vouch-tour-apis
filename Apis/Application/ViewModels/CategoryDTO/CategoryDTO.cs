using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.CategoryDTO
{
    public class CategoryViewDTO
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; } = default!;
    }

    public class CategoryCreateDTO
    {
        public string CategoryName { get; set; } = default!;
    }

    public class CategoryUpdateDTO : CategoryCreateDTO
    {
        public Guid Id { get; set; }
    }

}
