using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        public string ProductName {get;set;} = default!;
        public double ResellPrice {get;set;} = default!;
        public double RetailPrice {get;set;} = default!;
        public string Status {get;set;} = default!;
        public string Description { get;set;} = default!;

        // Images 
        public ICollection<ProductImage> Images { get; set; } = default!;
        // Supplier
        public Guid SupplierId { get; set;} = default!;
        public Supplier Supplier { get; set; } = default!;

        public Guid CategoryId { get; set; } = default!;
        public Category Category { get; set; } = default!;
        public ICollection<ProductInMenu> ProductInMenus { get; set;} = default!;

    }
}