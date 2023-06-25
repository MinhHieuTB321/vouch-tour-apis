using Application.GlobalExceptionHandling.Exceptions;
using Application.Interfaces;
using Application.ViewModels.MenuDTOs;
using Application.ViewModels.Product.ProductImage;
using Application.ViewModels.ProductInMenuDTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Services
{
    public class MenuService : IMenuService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsService _claimsService;
        private readonly IMapper _mapper;

        public MenuService(IUnitOfWork unitOfWork, IClaimsService claimsService, IMapper mapper)
        {
            _claimsService = claimsService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> AddListProductToMenu(Guid menuId,List<ProductMenuCreateDTO> productMenus)
        {
            var listCreate = _mapper.Map<List<ProductInMenu>>(productMenus);
            for (int i = 0; i < listCreate.Count; i++)
            {
                listCreate[i].MenuId = menuId;
            }
            await _unitOfWork.ProductMenuRepository.AddRangeAsync(listCreate);
            await _unitOfWork.SaveChangeAsync();
            return menuId;
        }


        public async Task<List<ProductMenuViewDTO>> GetProductInMenuViewAsync(Guid menuId)
        {
            var productMenus = await _unitOfWork.ProductMenuRepository.GetProductByMenuId(menuId);
            if(productMenus.Count>0)
            {
               return await GetProductInMenu(productMenus);
            }
            throw new NotFoundException("Not Found!");
        }

        private async Task<List<ProductMenuViewDTO>> GetProductInMenu(List<ProductInMenu> products)
        {
            var result = new List<ProductMenuViewDTO>();
            for (int i = 0; i < products.Count; i++)
            {
               var product= await GetProductMenuView(products[i]);
                product.ProductId = products[i].Id;
               result.Add(product);    
            }
            return result;
        }


        private async Task<ProductMenuViewDTO> GetProductMenuView(ProductInMenu productMenu)
        {
            var result = _mapper.Map<ProductMenuViewDTO>(productMenu);
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productMenu.ProductId!.Value, x => x.Supplier, x => x.Category, x => x.Images);
            if (product != null)
            {

                result = _mapper.Map(product, result);
                result.Images = _mapper.Map<List<ProductImageMenuViewDTO>>(product.Images);
            }
            return result;
        }

        public async Task<Guid> CreateMenu(MenuCreateDTO createDTO)
        {
            var newMenu = new Menu { Title = createDTO.Title,TourGuideId=_claimsService.GetCurrentUser };
            var result=await _unitOfWork.MenuRepository.AddAsync(newMenu);
            var listCreate = _mapper.Map<List<ProductInMenu>>(createDTO.ProductMenus);
            for (int i = 0; i < listCreate.Count; i++)
            {
                listCreate[i].MenuId = result.Id;
            }
            await _unitOfWork.ProductMenuRepository.AddRangeAsync(listCreate);
            await _unitOfWork.SaveChangeAsync();
            return result.Id;
        }

        public async Task<List<MenuListViewDTO>> GetAllMenu()
        {
            var listMenu= await _unitOfWork.MenuRepository.FindListByField(x=>x.CreatedBy==_claimsService.GetCurrentUser&&x.IsDeleted==false,x=>x.ProductInMenus!);
            if (listMenu.Count == 0) throw new NotFoundException("There is no menu!");
            listMenu= listMenu.OrderByDescending(x=>x.CreationDate).ToList();
            var result= _mapper.Map<List<MenuListViewDTO>>(listMenu);
            for (int i = 0; i < result.Count; i++)
            {
                result[i].NumOfProduct = listMenu[i].ProductInMenus!.Count;
            }
            return result;
        }

        public async Task<bool> DeleteMenu(Guid menuId)
        {
            var menu= await _unitOfWork.MenuRepository.GetByIdAsync(menuId);
            if (menu == null) throw new NotFoundException($"Can not found menu {menuId} ");
            menu.IsDeleted = true;
            _unitOfWork.MenuRepository.SoftRemove(menu);
            var result= await _unitOfWork.SaveChangeAsync();
            return result > 0;
        }

        public async Task UpdateMenu(MenuUdpateDTO updateDTO)
        {
            var menu =await _unitOfWork.MenuRepository.GetByIdAsync(updateDTO.MenuId);
            if (menu == null) throw new NotFoundException("Can not found menu " + updateDTO.MenuId + "!");
            var update = _mapper.Map(updateDTO, menu);
            
            _unitOfWork.MenuRepository.Update(update);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<MenuViewDTO> GetMenuViewById(Guid menuId)
        {
            var menu = await _unitOfWork.MenuRepository.GetByIdAsync(menuId, x => x.ProductInMenus!);
            if (menu == null) throw new NotFoundException("Not Found!");

            var result = _mapper.Map<MenuViewDTO>(menu);
            result.NumberOfProduct = menu.ProductInMenus!.Count;
            return result;
        }
    }
}
