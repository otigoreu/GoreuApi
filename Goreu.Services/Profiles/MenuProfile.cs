using AutoMapper;
using Goreu.Dto.Request;
using Goreu.Dto.Response;
using Goreu.Entities;

namespace Goreu.Services.Profiles
{
    public class MenuProfile : Profile
    {
        public MenuProfile()
        {
            CreateMap<Menu, MenuRequestDto>();
            CreateMap<Menu, MenuRequestDtoSingle>();
            CreateMap<MenuRequestDto, Menu>();
            CreateMap<MenuRequestDtoSingle, Menu>();

            CreateMap<Menu, MenuResponseDto>();
            CreateMap<Menu, MenuInfo>();


        }
    }
}
