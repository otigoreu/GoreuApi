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
                .ForMember(dest => dest.Descripcion_UnidadOrganica, opt => opt.MapFrom(src => src.UnidadOrganica.Descripcion))
                .ForMember(dest => dest.Hasta, opt => opt.MapFrom(src => src.Estado ? src.Hasta : src.FechaAnulacion))
                .ReverseMap();
        }
    }
}
