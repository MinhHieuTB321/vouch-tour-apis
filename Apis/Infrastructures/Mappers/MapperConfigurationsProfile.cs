
using AutoMapper;
using Application.Commons;
using Domain.Entities;
using Application.ViewModels;
using Application.ViewModels.Product;
using Application.ViewModels.Product.ProductImage;
using Application.ViewModels.SupplierDTO;
using Application.ViewModels.CategoryDTO;
using Application.ViewModels.TourGuideDTO;

namespace Infrastructures.Mappers
{
    public class MapperConfigurationsProfile : Profile
    {
        public MapperConfigurationsProfile()
        {
            #region UserMapping
            CreateMap<User, UserViewDTO>().ReverseMap();
            CreateMap<User,TourGuideCreateDTO>()
                .ForMember(des=>des.Name,options=>options.MapFrom(src=>src.FullName))
                .ForMember(des => des.Email, options => options.MapFrom(src => src.Email))
                .ForMember(des => des.Sex, options => options.MapFrom(src => src.Sex))
                .ForMember(des => des.DateOfBirth, options => options.MapFrom(src => src.DateOfBirth))
                .ForMember(des => des.PhoneNumber, options => options.MapFrom(src => src.PhoneNumber))
                .ReverseMap();
            CreateMap<User,SupplierCreateDTO>()
                .ForMember(des => des.SupplierName, options => options.MapFrom(src => src.FullName))
                .ForMember(des => des.Email, options => options.MapFrom(src => src.Email))
                .ForMember(des => des.PhoneNumber, options => options.MapFrom(src => src.PhoneNumber))
                .ReverseMap();
            #endregion

            #region ProductMapping
            CreateMap<CreateProductDTO, Product>().ReverseMap();
            CreateMap<ViewProductDTO, Product>()
                .ForMember(v => v.Images, r => r.MapFrom(x => x.Images))
                .ForMember(v => v.Supplier, r => r.MapFrom(x => x.Supplier))
                .ForMember(v => v.Category, r => r.MapFrom(x => x.Category))
                .ReverseMap();
            #endregion

            #region ProductImageMapping
            CreateMap<ProductImageViewDTO, ProductImage>().ReverseMap();
            #endregion

            #region SupplierMapping
            CreateMap<Supplier, SupplierViewDTO>().ReverseMap();
            CreateMap<SupplierCreateDTO, Supplier>().ReverseMap();
            CreateMap<SupplierUpdateDTO, Supplier>().ReverseMap();
            #endregion

            #region Categories
            CreateMap<Category, CategoryCreateDTO>().ReverseMap();
            CreateMap<CategoryViewDTO, Category>().ReverseMap();
            CreateMap<CategoryCreateDTO, Category>().ReverseMap();
            #endregion

            #region TourGuides 
            CreateMap<TourGuide, TourGuideViewDTO>().ReverseMap();
            CreateMap<TourGuide,TourGuideCreateDTO>().ReverseMap();
            #endregion
        }
    }
}
