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
        public string Address { get; set; } = default!;
        public Guid AdminId { get; set; } = default!;
    }

    public class TourGuideCreateDTO
    {
        public string Email { get; set; } = default!;
        public string Name { get; set; } = default!;
        public DateTime? DateOfBirth { get; set; } = default;
        public byte? Sex { get; set; }
        public string Address { get; set; } = default!;
        public string? PhoneNumber { get; set; } = default!;     
    }

    public class TourGuideUpdateDTO
     {
        public string Name { get; set; } = default!;
        public byte? Sex { get; set; }
        public string PhoneNumber { get; set; } = default!;
        public string Address { get; set; } = default!;

    }
}
