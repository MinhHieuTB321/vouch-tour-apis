﻿using Domain.Entities;
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
        public TourGuideReport ReportInMonth { get; set; } = default!;
    }

    public class TourGuideReport
    {
        public int NumberOfGroup { get; set; } = default!;
        public int NumberOfOrderCompleted { get; set; } = default!;
        public int NumberOfOrderWaiting { get; set; } = default!;
        public int NumberOfOrderCanceled { get; set; } = default!;
        public int NumberOfProductSold { get; set; } = default!;
        public int Point { get; set; } = default!;
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
