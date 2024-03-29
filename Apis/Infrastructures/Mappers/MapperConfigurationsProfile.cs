﻿
using AutoMapper;
using Application.Commons;
using Domain.Entities;
using Application.ViewModels;
using Application.ViewModels.Product;
using Application.ViewModels.Product.ProductImage;
using Application.ViewModels.SupplierDTO;
using Application.ViewModels.CategoryDTO;
using Application.ViewModels.TourGuideDTO;
using Application.ViewModels.GroupDTOs;
using Application.ViewModels.MenuDTOs;
using Application.ViewModels.ProductInMenuDTOs;
using Application.ViewModels.CartDTO;

namespace Infrastructures.Mappers
{
    public class MapperConfigurationsProfile : Profile
    {
        public MapperConfigurationsProfile()
        {
            #region UserMapping
            CreateMap<UserViewDTO,User>()
                .ForMember(x => x.Email, o => o.MapFrom(x => x.Email))
                .ForPath(x=>x.Role.RoleName,o=>o.MapFrom(x=>x.Role))
                .ReverseMap();
            #endregion

            #region ProductMapping
            CreateMap<CreateProductDTO, Product>()
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ProductName.Trim()))
                .ForMember(d => d.Description, o => o.MapFrom(s => s.Description.Trim()))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.Trim()))
                .ReverseMap();
            CreateMap<UpdateProductDTO, Product>()
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ProductName.Trim()))
                .ForMember(d => d.Description, o => o.MapFrom(s => s.Description.Trim()))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.Trim()))
                .ReverseMap();
            CreateMap<ViewProductDTO, Product>()
                .ForMember(v => v.Images, r => r.MapFrom(x => x.Images))
                .ForMember(v => v.Supplier, r => r.MapFrom(x => x.Supplier))
                .ForMember(v => v.Category, r => r.MapFrom(x => x.Category))
                .ReverseMap();
            #endregion

            #region ProductImageMapping
            CreateMap<ProductImageViewDTO, ProductImage>().ReverseMap();
            CreateMap<ProductImageMenuViewDTO, ProductImage>().ReverseMap();
            CreateMap<ImageViewDTO, ProductImage>().ReverseMap();
            #endregion

            #region SupplierMapping
            CreateMap<Supplier, SupplierViewDTO>().ReverseMap();
            CreateMap<SupplierCreateDTO, Supplier>()
                .ForMember(des => des.Email, opt => opt.MapFrom(src => src.Email.Trim()))
                .ForMember(des => des.SupplierName, opt => opt.MapFrom(src => src.SupplierName.Trim()))
                .ForMember(des => des.Address, opt => opt.MapFrom(src => src.Address.Trim()))
                .ForMember(des => des.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ReverseMap();
            CreateMap<SupplierUpdateDTO, Supplier>()
                .ForMember(des=>des.SupplierName,opt=>opt.MapFrom(src=>src.SupplierName.Trim()))
                .ForMember(des => des.Address, opt => opt.MapFrom(src => src.Address.Trim()))
                .ForMember(des => des.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ReverseMap();

            CreateMap<SupplierReportView, Supplier>().ReverseMap();
            #endregion

            #region Categories
            CreateMap<CategoryCreateDTO, Category>()
                .ForMember(x=>x.CategoryName,o=>o.MapFrom(x=>x.CategoryName.Trim()))
                .ReverseMap();
            CreateMap<CategoryViewDTO, Category>().ReverseMap();
            #endregion

            #region TourGuides 
            CreateMap<TourGuide, TourGuideViewDTO>().ReverseMap();
            CreateMap<TourGuideCreateDTO,TourGuide>()
                .ForMember(d=>d.Email,o=>o.MapFrom(s=>s.Email.Trim()))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name.Trim()))
                .ForMember(d => d.Address, o => o.MapFrom(s => s.Address.Trim()))
                .ReverseMap();
            CreateMap<TourGuideUpdateDTO, TourGuide>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name.Trim()))
                .ForMember(d => d.Address, o => o.MapFrom(s => s.Address.Trim()))
                .ReverseMap();
            #endregion

            #region GroupMapping
            CreateMap<Group,GroupViewDTO>().ReverseMap();
            CreateMap< GroupCreateDTO, Group>()
                .ForMember(x=>x.GroupName,o=>o.MapFrom(x=>x.GroupName.Trim()))
                .ForMember(d => d.Description, o => o.MapFrom(s => s.Description.Trim()))
                .ReverseMap();
            CreateMap<GroupUpdateDTO, Group>()
                .ForMember(d=>d.Id,o=>o.MapFrom(s=>s.Id))
                .ForMember(d => d.GroupName, o => o.MapFrom(s => s.GroupName.Trim()))
                .ForMember(d => d.Description, o => o.MapFrom(s => s.Description.Trim()))
                .ForMember(d => d.Quantity, o => o.MapFrom(s => s.Quantity))
                .ForMember(d => d.EndDate, o => o.MapFrom(s => s.EndDate))
                .ForMember(d => d.StartDate, o => o.MapFrom(s => s.StartDate))
                .ForMember(d => d.MenuId, o => o.MapFrom(s => s.MenuId))
                .ReverseMap();
            #endregion

            #region MenuMapping
            CreateMap<Menu, MenuViewDTO>()
                .ForMember(des => des.Id, opt => opt.MapFrom(opt => opt.Id))
                .ForMember(des => des.Title, opt => opt.MapFrom(opt => opt.Title))
                .ForMember(des => des.TourGuideId, opt => opt.MapFrom(opt => opt.TourGuideId))
                .ForMember(des => des.Status, opt => opt.MapFrom(opt => opt.Status))
                .ReverseMap();
            CreateMap<Menu, MenuListViewDTO>()
                .ForMember(des => des.Id, opt => opt.MapFrom(opt => opt.Id))
                .ForMember(des => des.Title, opt => opt.MapFrom(opt => opt.Title))
                .ForMember(des => des.TourGuideId, opt => opt.MapFrom(opt => opt.TourGuideId))
                .ForMember(des => des.Status, opt => opt.MapFrom(opt => opt.Status))
                .ReverseMap();
            CreateMap<MenuCreateDTO, Menu>()
                .ForMember(d => d.Title, o => o.MapFrom(s => s.Title.Trim()))
                .ReverseMap();
            CreateMap<MenuUdpateDTO, Menu>()
                .ForMember(d => d.Title, o => o.MapFrom(s => s.Title.Trim()))
                .ReverseMap();
            #endregion

            #region ProductInMenu
            CreateMap<ProductInMenu,ProductMenuViewDTO>()
                .ForMember(v => v.MenuId, r => r.MapFrom(x => x.MenuId))
                .ForMember(v => v.ActualPrice, r => r.MapFrom(x => x.ActualPrice))
                .ForMember(v => v.SupplierPrice, r => r.MapFrom(x => x.SupplierPrice))
                .ForMember(v=>v.Description,r=>r.MapFrom(x => x.Description))
                .ReverseMap();

            CreateMap<ProductMenuViewDTO, Product>()
                .ForPath(v => v.ProductName, r => r.MapFrom(x => x.ProductName))
                .ForPath(v => v.Description, r => r.MapFrom(x => x.Description))
                .ForPath(v => v.Category.CategoryName, r => r.MapFrom(x => x.CategoryName))
                .ForPath(v => v.Supplier.SupplierName, r => r.MapFrom(x => x.SupplierName))
                .ForPath(v => v.Supplier.Address, r => r.MapFrom(x => x.Address))
                .ForPath(v => v.Supplier.Id, r => r.MapFrom(x => x.SupplierId))
                .ReverseMap();


            CreateMap<ProductMenuCreateDTO, ProductInMenu>()
                .ForMember(v => v.ProductId, r => r.MapFrom(x => x.ProductId))
                .ForMember(v => v.ActualPrice, r => r.MapFrom(x => x.ActualPrice))
                .ForMember(v => v.SupplierPrice, r => r.MapFrom(x => x.SupplierPrice))
                .ForMember(v => v.Description, r => r.MapFrom(x => x.Description.Trim()))
                .ReverseMap();
            #endregion

            #region CartItemMapping
            CreateMap<ItemViewDTO, Product>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.ProductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ProductName))
                .ForMember(d=>d.Description,o=>o.MapFrom(s => s.Description))
                .ForPath(d => d.Supplier.SupplierName, o => o.MapFrom(s => s.SupplierName))
                .ForPath(d => d.Supplier.Id, o => o.MapFrom(s => s.SupplerId))
                .ForPath(d => d.Category.CategoryName, o => o.MapFrom(s => s.CategoryName))
                .ForMember(d => d.ResellPrice, o => o.MapFrom(s => s.SuppleirPrice))
                .ReverseMap();

            #endregion

            #region OrderMapping
            CreateMap<OrderCreateDTO, Order>()
                .ForMember(d=>d.GroupId,o=>o.MapFrom(s=>s.GroupId))
                .ForMember(d => d.CustomerName, o => o.MapFrom(s => s.CustomerName.Trim()))
                .ForMember(d => d.PhoneNumber, o => o.MapFrom(s => s.PhoneNumber))
                .ForMember(d => d.Note, o => o.MapFrom(s => s.Note.Trim()))
                .ReverseMap();
            CreateMap<OrderViewDTO, Order>()
                .ForPath(d => d.Group.Id, o => o.MapFrom(s => s.GroupId))
                .ForPath(d => d.Group.GroupName, o => o.MapFrom(s => s.GroupName))
                .ReverseMap();
            #endregion

            #region OrderDetailMapping
            CreateMap<OrderDetailCreateDTO, OrderDetail>().ReverseMap();
            CreateMap<OrderDetailViewDTO, OrderDetail>()
                .ForPath(d => d.Product.ProductName, o => o.MapFrom(s => s.ProductName))
                .ForPath(d => d.Order.ShipAddress, o => o.MapFrom(s => s.ShipAddress))
                .ReverseMap();

            CreateMap<OrderDetailUpdateDTO, OrderDetail>().ReverseMap();
            #endregion

            #region Payment
            CreateMap<PaymentViewDTO, Payment>().ReverseMap();
            #endregion
        }
    }
}
