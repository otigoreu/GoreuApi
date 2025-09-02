using AutoMapper;
using Goreu.Dto.Request;
using Goreu.Dto.Response;
using Goreu.Entities;

namespace Goreu.Services.Profiles
{
    public class UsuarioUnidadOrganicaProfile : Profile
    {
        public UsuarioUnidadOrganicaProfile()
        {
            CreateMap<UsuarioUnidadOrganica, UsuarioUnidadOrganicaResponseDto>();
            CreateMap<UnidadOrganicaRequestDto, UnidadOrganica>();
            CreateMap<UsuarioUnidadOrganicaRequestDto, UsuarioUnidadOrganica>();

            CreateMap<UsuarioUnidadOrganica, UsuarioUnidadOrganica_UnidadOrganicaResponseDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.IdUsuario))
                .ForMember(dest => dest.Desde, opt => opt.MapFrom(src => src.Desde == default ? DateTime.Now : src.Desde))
                .ForMember(dest => dest.Hasta, opt => opt.MapFrom(src => src.Hasta ?? DateTime.Now))
                .ForMember(dest => dest.Descripcion_UnidadOrganica, opt => opt.MapFrom(src => src.UnidadOrganica.Descripcion))
                .ReverseMap();
        }
    }
}
