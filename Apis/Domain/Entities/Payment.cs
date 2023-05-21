using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Payment : BaseEntity
    {
        public string PaymentName {get;set;} = default!;
        public string Status {get;set;} = default!;

        public Guid OrderId {get;set;} 
        public Order Order {get;set;} = default!;
    }
}