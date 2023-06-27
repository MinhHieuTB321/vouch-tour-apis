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
        Task<List<ProductMenuViewDTO>> GetProductInMenuViewAsync(Guid menuId);

        Task<Guid> AddListProductToMenu(Guid menuId,List<ProductMenuCreateDTO> productMenus);

        Task<Guid> CreateMenu(MenuCreateDTO createDTO);

        Task<List<MenuListViewDTO>> GetAllMenu();

        Task<bool> DeleteMenu(Guid menuId);

        Task UpdateMenu(MenuUdpateDTO updateDTO);

        Task<MenuViewDTO> GetMenuViewById(Guid menuId);

        Task<ProductMenuViewDTO> GetProductInMenuById(Guid menuId,Guid productId);

        Task<bool> DeleteProductFromMenu(Guid MenuId,Guid productMenuId);
    }

   
}
