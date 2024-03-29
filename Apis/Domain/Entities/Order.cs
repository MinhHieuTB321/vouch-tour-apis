using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order : BaseEntity
    {
        public double TotalPrice { get; set;}
        public string Status { get; set; } =OrderEnums.Waiting.ToString();
        public string CustomerName { get; set; } = default!;
        public string PhoneNumber { get; set; }=default!;
        public string Note { get; set; } = default!;
        public string? ShipAddress { get; set; } = default!;
        public Guid TourGuideId {  get; set; }
        public Guid GroupId { get;set; }
        public Group Group {get;set;} = default!;
        public ICollection<OrderDetail> OrderDetails {get;set;} = default!;
        public ICollection<Payment> Payments {get;set;} = default!;
    }
}