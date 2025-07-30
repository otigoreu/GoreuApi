using AutoMapper;
using Goreu.Dto.Request;
using Goreu.Dto.Response;
using Goreu.Entities;

namespace Goreu.Services.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioResponseDto>()
                .ForMember(dest => dest.descripcionPersona,
                    opt => opt.MapFrom(src => $"{src.Persona.ApellidoPat} {src.Persona.ApellidoMat}, {src.Persona.Nombres}"))
                .ForMember(dest => dest.CantidadUnidadorganicas,
                        opt => opt.MapFrom(src => src.UsuarioUnidadOrganicas.Count));
        }
    }
}
