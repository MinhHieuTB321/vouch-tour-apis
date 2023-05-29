using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class UserViewDTO
    {
        public String FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }
        public bool Sex { get; set; }

    }
}
