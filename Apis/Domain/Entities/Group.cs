using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Group : BaseEntity
    {
        public string GroupName {get;set;} = default!;
        public int Quantity {get;set;} = default!;
        public DateTime EndDate {get;set;}

        public Guid TourGuideId {get;set;}
        public User TourGuide {get;set;} = default!;
        public ICollection<Order> Orders {get;set;} = default!;
    }
}