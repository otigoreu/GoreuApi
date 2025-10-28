namespace Goreu.Services.Profiles
{
    public class UserRoleProfile : Profile
    {
        public UserRoleProfile()
        {
            CreateMap<Usuario, UsuarioRol_UsuarioResponseDto>()
                .ForMember(dest => dest.NombreCompleto,
                    opt => opt.MapFrom(src => $"{src.Persona.ApellidoPat} {src.Persona.ApellidoMat}, {src.Persona.Nombres}"))
                .ForMember(dest => dest.CantidadUnidadOrganica,
                        opt => opt.MapFrom(src => src.UsuarioUnidadOrganicas.Count))
                .ForMember(dest => dest.CantidadRol,
                        opt => opt.MapFrom(src => src.UsuarioRoles.Count))
                .ForMember(dest => dest.MustChangePassword,
                        opt => opt.MapFrom(src => src.MustChangePassword));
        }
    }
}
