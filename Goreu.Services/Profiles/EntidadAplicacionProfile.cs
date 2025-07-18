using AutoMapper;
using Goreu.Dto.Request;
using Goreu.Dto.Response;
using Goreu.Entities;

namespace Goreu.Services.Profiles
{
    public class EntidadAplicacionProfile : Profile
    {
        public EntidadAplicacionProfile()
        {
            CreateMap<EntidadAplicacion, EntidadAplicacionResponseDto>();
            CreateMap<EntidadAplicacionRequestDto, EntidadAplicacion>();
        }
    }
}
