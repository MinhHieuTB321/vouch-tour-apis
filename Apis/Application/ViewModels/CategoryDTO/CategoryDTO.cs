using Microsoft.AspNetCore.Http;
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
        public string FileName { get; set; } = default!;
        public string URL { get; set; } = default!;
        public string CategoryName { get; set; } = default!;
    }

    public class CategoryCreateDTO
    {
        public string CategoryName { get; set; } = default!;
        public IFormFile File { get; set; } = default!;
    }

    public class CategoryUpdateDTO 
    {
        public Guid Id { get; set; }
        public IFormFile File { get; set; } = default!;
        public string CategoryName { get; set; } = default!;
    }

}
