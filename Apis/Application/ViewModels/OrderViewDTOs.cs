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
        public string Status { get; set; } = "Created";
        public string CustomerName { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Note { get; set; } = default!;
        public DateTime CreationDate { get;set; }
        public string PaymentName { get; set; } = default!;
        public Guid GroupId { get; set; }
        public string GroupName { get; set; } = default!;
        //public GroupViewDTO Group { get; set; } = default!;
        public ICollection<OrderDetailViewDTO> OrderDetails { get; set; } = default!;    
    }



    public class OrderDetailViewDTO
    {
        public Guid ProductMenuId { get; set; }
        public string ProductName { get; set; }=default!;
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }

    }
    public class OrderCreateDTO
    {
        public Guid GroupId { get; set; }
        public string CustomerName { get; set; } = default!;
        public string PhoneNumber { get; set; } =default!;
        public string Note { get;set; } =default!;
        public List<OrderDetailCreateDTO> OrderProductDetails { get; set; } = default!;
    }

    public class OrderDetailCreateDTO
    {
        public Guid ProductMenuId { get; set; }
        public string ProductName { get;set; } =default!;
        public int Quantity { get;set; }
        public double UnitPrice { get;set; }
    }

    public class OrderUpdateDTO
    {
        public Guid Id { get; set; }
        public string Status { get;set; } =default!;
    }

} 
