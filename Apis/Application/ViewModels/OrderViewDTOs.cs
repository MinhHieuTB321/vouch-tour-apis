using Application.ViewModels.GroupDTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class OrderViewDTO
    {
        public Guid Id { get; set; }
        public double TotalPrice { get; set; }
        public string CustomerName { get; set; } = default!;
        public GroupViewDTO Group { get; set; } = default!;
        public ICollection<OrderDetail> OrderDetails { get; set; } = default!;
        public ICollection<PaymentViewDTO> Payments { get; set; } = default!;

    }
}
