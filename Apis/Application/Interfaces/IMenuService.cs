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
        Task<MenuViewDTO> GetMenuViewAsync(Guid menuId);

        Task<Guid> AddListProductToMenu(Guid menuId,List<ProductMenuCreateDTO> productMenus);

        Task<Guid> CreateMenu(MenuCreateDTO createDTO);

        Task<List<MenuListViewDTO>> GetAllMenu();
        Task<bool> DeleteMenu(Guid menuId);

        Task UpdateMenu(MenuUdpateDTO updateDTO);
    }

   
}
