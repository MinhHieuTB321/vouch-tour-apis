using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    // Modified
    public class Admin : BaseEntity
    {
        public string Name { get; set; } = default!;
        public DateTime DateOfBirth { get; set; } = default;
        public byte? Sex { get; set; }
        public string PhoneNumber { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Status { get; set; } = default!;

        public ICollection<TourGuide> TourGuides { get; set; } = default!;
        public ICollection<Supplier> Suppliers { get; set; } = default!;
    }
}
