using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class UserViewDTO
    {
        public string Email { get; set; } = default!;
        public string Role { get; set; }=default!;

    }
}
