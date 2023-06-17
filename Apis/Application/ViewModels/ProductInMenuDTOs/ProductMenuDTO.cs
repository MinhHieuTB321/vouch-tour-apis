using Application.ViewModels.Product.ProductImage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.ProductInMenuDTOs
{
    public class ProductMenuViewDTO
    {
        public Guid MenuId { get; set; }
        public string ProductName = default!;
        public double ActualPrice { get; set; }
        public double SupplierPrice { get; set; }
        public string Description { get; set; } = default!;
        public Guid SupplierId { get; set; } = default!;
        public string SupplierName { get; set; } = default!;
        public string CategoryName { get; set; } = default!;
        public string Address { get; set; } = default!;
        public List<ProductImageMenuViewDTO> Images{ get; set; } = default!;
    }

    public class ProductMenuCreateDTO
    {
        public Guid ProductId { get; set; }
        public double ResellPrice { get;set; }
    }

}
