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
        }
    }
}
