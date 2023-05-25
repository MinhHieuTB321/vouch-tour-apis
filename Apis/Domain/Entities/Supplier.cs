using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Supplier : BaseEntity
    {
        public string Address { get; set; } = default!;
        public string SupplierName { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        
        public ICollection<Product> Products { get; set; } = default!;

        public Guid AdminId { get; set; }
        public Admin Admin { get; set; } = default!;
    }
}
