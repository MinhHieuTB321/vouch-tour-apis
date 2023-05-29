
using AutoMapper;
using Application.Commons;
using Domain.Entities;
using Application.ViewModels;

namespace Infrastructures.Mappers
{
    public class MapperConfigurationsProfile : Profile
    {
        public MapperConfigurationsProfile()
        {
            CreateMap<User, UserViewDTO>().ReverseMap();
        }
    }
}
