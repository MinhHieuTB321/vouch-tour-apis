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

        public async Task<Guid> AddProductToMenu(Guid menuId,List<ProductMenuCreateDTO> productMenus)
        {
            var listCreate = _mapper.Map<List<ProductInMenu>>(productMenus);
            for (int i = 0; i < listCreate.Count; i++)
            {
                listCreate[i].MenuId = menuId;
            }
            await _unitOfWork.ProductMenuRepository.AddRangeAsync(listCreate);
            await UpdateQuantityInMenu(menuId, productMenus.Count);
            await _unitOfWork.SaveChangeAsync();
            return menuId;
        }

        private async Task UpdateQuantityInMenu(Guid menuId,int quantity)
        {
            var menu= await _unitOfWork.MenuRepository.GetByIdAsync(menuId);
            if(menu != null) 
            {
                menu.Quantity = quantity;
                _unitOfWork.MenuRepository.Update(menu);
            }
        }

        public async Task<MenuViewDTO> GetMenuViewAsync(Guid groupId)
        {
            var menu = await _unitOfWork.MenuRepository.FindByField(x => x.GroupId == groupId && x.TourGuideId==_claimsService.GetCurrentUser);
            var productMenus = await _unitOfWork.ProductMenuRepository.GetProductByMenuId(menu.Id);
            var result = _mapper.Map<MenuViewDTO>(menu);
            if(menu!=null)
            {
                result.Products =await GetProductInMenu(productMenus);
                return result;
            }
            throw new NotFoundException("Not Found!");
        }

        private async Task<List<ProductMenuViewDTO>> GetProductInMenu(List<ProductInMenu> products)
        {
            var result = new List<ProductMenuViewDTO>();
            for (int i = 0; i < products.Count; i++)
            {
               var product= await GetProductMenuView(products[i]);
               result.Add(product);    
            }
            return result;
        }


        private async Task<ProductMenuViewDTO> GetProductMenuView(ProductInMenu productMenu)
        {
            var result = _mapper.Map<ProductMenuViewDTO>(productMenu);
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productMenu.ProductId.Value, x => x.Supplier, x => x.Category, x => x.Images);
            if (product != null)
            {

                result = _mapper.Map(product, result);
                result.Images = _mapper.Map<List<ProductImageMenuViewDTO>>(product.Images);
            }
            return result;
        }

        
    }
}
