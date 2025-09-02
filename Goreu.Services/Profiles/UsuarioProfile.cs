namespace Goreu.Services.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioResponseDto>()
                .ForMember(dest => dest.DescripcionPersona,
                    opt => opt.MapFrom(src => $"{src.Persona.ApellidoPat} {src.Persona.ApellidoMat}, {src.Persona.Nombres}"))
                .ForMember(dest => dest.CantidadUnidadOrganica,
                        opt => opt.MapFrom(src => src.UsuarioUnidadOrganicas.Count));

            CreateMap<UsuarioInfo, UsuarioResponseDto>()
                .ForMember(dest => dest.DescripcionPersona,
                    opt => opt.MapFrom(src => $"{src.ApellidoPat} {src.ApellidoMat}, {src.Nombres}"));
        }
    }
}
