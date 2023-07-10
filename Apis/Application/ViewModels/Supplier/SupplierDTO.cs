using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.SupplierDTO
{
    public class SupplierViewDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = default!;
        public string SupplierName { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;

    }

    public class SupplierReport
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = default!;
        public string SupplierName { get; set; } = default!;
        public int NumberOfProducts { get; set; }
        public int NumberOfProductInMenu { get; set; }
        public int NumberOfProductOutMenu { get; set; }
        public int NumberOfProductSold { get; set; }
        public double TotalMoneySold { get; set; }
    }

    public class SupplierCreateDTO
    {
        public string Email { get; set; } = default!;
        public string SupplierName { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
    }

    public class  SupplierUpdateDTO
    {
        public string SupplierName { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;

    }

   
    
                
    
}
