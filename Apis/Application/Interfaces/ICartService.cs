using Application.ViewModels.CartDTO;
using Application.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICartService
    {
        Task<bool> AddToCart(ItemAddDTO addDTO);
        Task<List<ItemViewDTO>> GetAllItems();
        Task<bool> UpdateItem(ItemUpdateDTO updateDTO);
        Task<bool> DeleteItem(string cartId,string id);
        Task<ItemViewDTO> GetItemById(String cartid,string id);
        Task<List<ViewProductDTO>> GetAllProductOutCart(string cartId);
        Task<bool> DeleteCart(string cartId);
       // Task DemoNoti();
    }
}
