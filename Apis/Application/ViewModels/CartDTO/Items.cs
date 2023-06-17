using Application.ViewModels.Product.ProductImage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.CartDTO
{
    public class ItemViewDTO
    {
        public string Id { get; set; } = default!;
        public string CartId { get; set; }=default!;
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public Guid SupplerId { get; set; } =default!;
        public string SupplierName { get; set; } = default!;
        public string CategoryName { get; set; } = default!;
        public double ActualPrice { get; set; }
        public double SuppleirPrice { get; set; }
        public string Description { get; set; } = default!;
        public List<ImageViewDTO> Images { get; set; } = default!;
    }
    public class ItemAddDTO
    {
        public Guid ProductId { get; set; }
        public Double ActualPrice { get; set; }
    }
    public class ItemUpdateDTO
    {
        public string Id { get; set; } = default!;
        public Double ActualPrice { get; set; }
    }
}
