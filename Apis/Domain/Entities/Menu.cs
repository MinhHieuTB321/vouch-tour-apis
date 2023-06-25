using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class Menu : BaseEntity
    {
        public string Title { get; set; } = default!;
//        public int Quantity {get;set;}
        public string Status {get;set;} = nameof(ActiveEnum.Active);

        public Guid TourGuideId {get;set;} 
        public TourGuide TourGuide {get;set;} = default!;

        public ICollection<ProductInMenu>? ProductInMenus {get;set;} = default!;
        //public Guid? GroupId { get;set;}
        //public Group? Group { get; set; } = default!;

        public ICollection<Group> Groups { get; set; } = default!;

    }
}

