using AutoMapper;
using Goreu.Dto.Request;
using Goreu.Dto.Response;
using Goreu.Entities;
using Goreu.Entities.Info;

namespace Goreu.Services.Profiles
{
    public class AplicacionProfile : Profile
    {
        public AplicacionProfile()
        {
            CreateMap<Aplicacion, AplicacionResponseDto>();
            CreateMap<AplicacionRequestDtoSingle, Aplicacion>();
            CreateMap<AplicacionRequestDto, Aplicacion>();
            CreateMap<AplicacionInfo, AplicacionResponseDto>();
        }
    }
}
