using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderDetail : BaseEntity
    {
        public string ProductName { get; set; } = default!;
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public Guid TourGuideId { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = default!;
        public Guid ProductMenuId { get; set; }
        public ProductInMenu ProductMenu { get; set; } = default!;
   
    }
}