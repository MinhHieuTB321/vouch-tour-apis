using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Category : BaseEntity
    {
        public string CategoryName {get;set;} = default!;
        public string FileName { get;set;} = default!;
        public string URL { get;set;} = default!;
        public ICollection<Product> Products {get;set;} = default!;
    }
}