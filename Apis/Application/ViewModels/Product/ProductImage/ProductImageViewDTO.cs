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
        public string URL { get; set; } = default!;
        public string FileName { get; set; } = default!;

        public Guid ProductId { get; set; } = default!;
    }
}
