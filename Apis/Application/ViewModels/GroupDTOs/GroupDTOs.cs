using Application.ViewModels.TourGuideDTO;
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
        public int Quantity { get; set; }
        public TourGuideViewDTO TourGuide { get; set; } = default!;
        
    }

    public class GroupCreateDTO
    {

    }
    public class GroupUpdateDTO : GroupCreateDTO
    {

    }
}
