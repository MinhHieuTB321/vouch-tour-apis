using Application.ViewModels.CategoryDTO;
using Application.ViewModels.Product.ProductImage;
using Application.ViewModels.SupplierDTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Product
{
    public class ViewProductDTO
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = default!;
        public double ResellPrice { get; set; } = default!;
        public double RetailPrice { get;set; } = default!;
        public string Description { get; set; } = default!; 
        public string Status { get; set; } = default!;
        public ICollection<ProductImageViewDTO> Images { get; set; } = default!;
        public SupplierViewDTO Supplier { get; set; } = default!;
        public CategoryViewDTO Category { get; set; } = default!;
    }
}
