using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; } = default!;
        public bool Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; } = default!;
        public string Email { get; set; } = default!;

        public int RoleId {get;set;}
        public Role Role {get;set;} = default!;


        // Configuration For Supplier
        public ICollection<Product> Products {get;set;} = default!;

        // Configuration For TourGuide
        public ICollection<Group> Groups {get;set;} = default!;
        public ICollection<Menu> Menus {get;set;} = default!;
    }
}