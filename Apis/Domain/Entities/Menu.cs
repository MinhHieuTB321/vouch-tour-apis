using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class Menu : BaseEntity
    {
        public int Quantity {get;set;}
        public string Status {get;set;} = nameof(ActiveEnum.Active);

        public Guid TourGuideId {get;set;} 
        public User TourGuide {get;set;} = default!;

        public ICollection<DiscountProduct> DiscountProducts {get;set;} = default!;
    }
}