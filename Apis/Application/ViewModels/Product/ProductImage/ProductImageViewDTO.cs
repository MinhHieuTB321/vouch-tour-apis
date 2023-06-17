using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Product.ProductImage
{
    public class ProductImageViewDTO
    {
        public Guid Id { get; set; }
        public string FileURL { get; set; } = default!;
        public string FileName { get; set; } = default!;

        public Guid ProductId { get; set; } = default!;
    }

    public class ProductImageMenuViewDTO
    {
        public string FileURL { get; set; } = default!;
        public string FileName { get; set; } = default!;
    }

    public class ImageViewDTO
    {
        public string FileName { get; set; } = default!;
        public string FileURL { get; set; } = default!;
    }
}
