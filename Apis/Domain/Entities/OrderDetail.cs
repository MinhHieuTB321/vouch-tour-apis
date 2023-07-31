using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderDetail : BaseEntity
    {
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public Guid SupplierId { get; set; }
        public string Status { get; set; } = OrderDetailEnums.Waiting.ToString();
        public string? Reason { get; set; } = default!;
        public Guid TourGuideId { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = default!;
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = default!;
   
    }
}