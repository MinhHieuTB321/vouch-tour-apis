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

        // Supplier
        public Guid SupplierId {get;set;}
        public User Supplier {get;set;} = default!;

        public Guid CategoryId {get;set;}
        public Category Category {get;set;} = default!;
        public ICollection<DiscountProduct> DiscountProducts {get;set;} = default!;
    }
}