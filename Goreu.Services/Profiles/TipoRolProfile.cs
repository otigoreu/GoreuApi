using AutoMapper;
using Goreu.Dto.Request;
using Goreu.Dto.Response;
using Goreu.Entities;

namespace Goreu.Services.Profiles
{
    public class TipoRolProfile:Profile
    {
        public TipoRolProfile() {
            CreateMap<TipoRol,TipoRolResponseDto>();
            CreateMap<TipoRolRequestDto,TipoRol>();
        }
    }
}
