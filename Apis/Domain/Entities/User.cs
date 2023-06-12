using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; } = default!;
        public Guid UserId { get; set; }
        public int RoleId {get;set;}
        public Role Role {get;set;} = default!;

    }
}