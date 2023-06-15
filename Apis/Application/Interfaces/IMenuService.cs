using Application.ViewModels.MenuDTOs;
using Application.ViewModels.ProductInMenuDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMenuService
    {
        Task<MenuViewDTO> GetMenuViewAsync(Guid groupId);

        Task<Guid> AddProductToMenu(Guid menuId,List<ProductMenuCreateDTO> productMenus);

        
    }
}
