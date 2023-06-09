using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class PaymentViewDTO
    {
        public Guid Id { get; set; }
        public string PaymentName { get; set; } = default!;
        public string Status { get; set; } = default!;

    }
}
