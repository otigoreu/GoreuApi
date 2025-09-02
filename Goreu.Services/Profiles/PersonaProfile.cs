using AutoMapper;
using Goreu.Dto.Request;
using Goreu.Dto.Response;
using Goreu.Entities;
using Goreu.Entities.Info;

namespace Goreu.Services.Profiles
{
    public class PersonaProfile : Profile
    {
        public PersonaProfile()
        {
            CreateMap<PersonaInfo, PersonaResponseDto>();
            CreateMap<Persona, PersonaResponseDto>()
                .ForMember(d => d.NombreCompleto, o => o.MapFrom(x => $"{x.Nombres} {x.ApellidoPat} {x.ApellidoMat}"));

            //CreateMap<PersonaRequestDto, Persona>()
            //    .ForMember(d => d.FechaNac, o => o.MapFrom(x => DateOnly.Parse($"{x.fechaNac}")));
            CreateMap<PersonaRequestDto, Persona>();
            CreateMap<Persona, PersonaInfo>();
        }
    }
}
