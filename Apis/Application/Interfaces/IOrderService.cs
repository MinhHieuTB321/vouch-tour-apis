using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Task<bool> CreateOrder(OrderCreateDTO orderCreate);
        Task<OrderViewDTO> GetOrderById(Guid orderId);
        Task<bool> UpdateOrder(OrderUpdateDTO updateDTO);
        Task<bool> DeleteOrder(Guid orderId);
    }
}
