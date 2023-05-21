using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderDetail : BaseEntity
    {
        public Guid DiscountProductId {get;set;}
        public Guid OrderId {get;set;}

        public Order Order {get;set;} = default!;
        public DiscountProduct DiscountProduct {get;set;} = default!;
        
    }
}