using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DiscountProduct : BaseEntity
    {
        public double ActualPrice {get;set;}
        public DateTime DateExpired {get;set;}
        public int Quantity {get;set;}
        public string Status {get;set;} = default!;

        public Guid MenuId {get;set;} 
        public Menu Menu {get;set;} = default!;

        public Guid ProductId {get;set;} 
        public Product Product {get;set;} = default!;

        public ICollection<OrderDetail> OrderDetails {get;set;} = default!;
    }
}