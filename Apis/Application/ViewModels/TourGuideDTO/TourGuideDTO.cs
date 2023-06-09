using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.TourGuideDTO
{
    public class TourGuideViewDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public byte? Sex { get; set; }
        public string PhoneNumber { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Status { get; set; } = default!;

        public Guid AdminId { get; set; } = default!;
    }

    public class TourGuideCreateDTO
    {

    }

    public class TourGuideUpdateDTO : TourGuideCreateDTO
    {

    }
}
