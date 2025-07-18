using AutoMapper;
using Goreu.Dto.Request;
using Goreu.Dto.Response;
using Goreu.Entities;

namespace Goreu.Services.Profiles
{
    public class HistorialProfile : Profile
    {
        public HistorialProfile()
        {
            CreateMap<Historial, HistorialResponseDto>();
            CreateMap<HistorialRequestDto, Historial>();
        }
    }
}
