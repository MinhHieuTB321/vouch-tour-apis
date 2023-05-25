using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order : BaseEntity
    {
        public double TotalPrice { get; set;}
        public string Status { get; set; } = "Created";
        public string CustomerName { get; set; } = default!;
        public Guid GroupId { get;set; }
        public Group Group {get;set;} = default!;
        public ICollection<OrderDetail> OrderDetails {get;set;} = default!;
        public ICollection<Payment> Payments {get;set;} = default!;
    }
}