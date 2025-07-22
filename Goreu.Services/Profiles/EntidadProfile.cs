using AutoMapper;
using Goreu.Dto.Request;
using Goreu.Dto.Response;
using Goreu.Entities;
using Goreu.Entities.Info;

namespace Goreu.Services.Profiles
{
    public class EntidadProfile : Profile
    {
        public EntidadProfile()
        {
            CreateMap<Entidad, EntidadResponseDto>()
                .ForMember(dest => dest.CantidadAplicaciones, opt => opt.MapFrom(src => src.EntidadAplicaciones.Count));

            CreateMap<EntidadRequestDto, Entidad>();
            CreateMap<EntidadInfo, EntidadResponseDto>();
        }
    }
}
