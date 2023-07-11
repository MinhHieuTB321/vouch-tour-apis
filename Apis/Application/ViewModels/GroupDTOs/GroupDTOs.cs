using Application.ViewModels.TourGuideDTO;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.GroupDTOs
{
    public class GroupViewDTO
    {
        public Guid Id { get; set; }
        public string GroupName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int Quantity { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public string Status { get; set; } = default!;
        public Guid MenuId { get; set; }
        
    }

    public class GroupCreateDTO
    {
        public string GroupName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int Quantity { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
    }
    public class GroupUpdateDTO
    {
        public Guid Id { get; set; }
        public string GroupName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int Quantity { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public Guid MenuId { get; set; }
    }

    public class GroupMenuDTO
    {
        public Guid Id { get; set; } 
        public Guid MenuId { get; set;}
    }
}
