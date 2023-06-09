using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Product
{
    public class CreateProductDTO
    {
        public string ProductName { get; set; } = default!;
        public double ResellPrice { get; set; } = default!;
        public double RetailPrice { get; set; } = default!;
        public string Status { get; set; } = "Active";
        public IEnumerable<IFormFile?> File { get; set; } = default!;
        public Guid SupplierId { get; set; } = default!;
        public Guid CategoryId { get; set; } = default!;
    }

    public class UpdateProductDTO : CreateProductDTO
    {
        public Guid ProductId { get; set; }
    }
}
