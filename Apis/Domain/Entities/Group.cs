using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Group : BaseEntity
    {
        public string GroupName {get;set;} = default!;
        public string Description { get;set;} = default!;
        public int Quantity {get;set;} = default!;
        public DateTime StartDate { get;set;} = default!;
        public DateTime EndDate {get;set;}
        public Guid TourGuideId {get;set;}
        public TourGuide TourGuide {get;set;} = default!;
        public ICollection<Order> Orders {get;set;} = default!;

        public Guid? MenuId { get; set;} = default!; 
        public Menu? Menu { get; set;} = default!;
    }
}