using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TourGuide : BaseEntity
    {
        public string Name { get; set; } = default!;
        public DateTime DateOfBirth { get; set; } = default;
        public byte? Sex { get; set; }
        public string PhoneNumber { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Status { get; set; } = default!;

        public ICollection<Group> Groups { get; set; } = default!;
        public ICollection<Menu> Menus { get; set; } = default!;

        public Guid AdminId { get; set; }
        public Admin Admin { get; set; } = default!;

    }
}
