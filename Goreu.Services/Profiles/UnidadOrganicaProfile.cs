using AutoMapper;
using Goreu.Dto.Request;
using Goreu.Dto.Response;
using Goreu.Entities;
using Goreu.Entities.Info;

namespace Goreu.Services.Profiles
{
    public class UnidadOrganicaProfile : Profile
    {
        public UnidadOrganicaProfile()
        {
            CreateMap<UnidadOrganica, UnidadOrganicaResponseDto>()
            .ForMember(dest => dest.NombreEntidad,
                       opt => opt.MapFrom(src => src.Entidad != null ? src.Entidad.Descripcion : string.Empty))
            .ForMember(dest => dest.NombreDependencia,
                       opt => opt.MapFrom(src => src.Dependencia != null ? src.Dependencia.Descripcion : ""))
            .ForMember(dest => dest.CantidadHijos,
                       opt => opt.MapFrom(src => src.Hijos != null ? src.Hijos.Count : 0));

            CreateMap<UnidadOrganicaRequestDto, UnidadOrganica>()
                .ForMember(dest => dest.IdDependencia,
                       opt => opt.MapFrom(src => src.IdUnidadOrganicaPadre));
            CreateMap<UnidadOrganicaInfo,UnidadOrganicaResponseDto>();
            CreateMap<UnidadOrganicaInfo, UnidadOrganicaResponseSingleDto>();
        }
    }
}
