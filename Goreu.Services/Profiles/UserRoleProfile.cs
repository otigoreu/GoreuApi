namespace Goreu.Services.Profiles
{
    public class UserRoleProfile : Profile
    {
        public UserRoleProfile()
        {
            CreateMap<UsuarioRol, UsuarioRol_UsuarioResponseDto>()

                .ForMember(dest => dest.UserName,
                    opt => opt.MapFrom(src => src.Usuario.UserName))
                .ForMember(dest => dest.Rol_Descripcion,
                    opt => opt.MapFrom(src => src.Rol.Name))
                .ForMember(dest => dest.NombreCompleto,
                    opt => opt.MapFrom(src => $"{src.Usuario.Persona.ApellidoPat} {src.Usuario.Persona.ApellidoMat}, {src.Usuario.Persona.Nombres}"))
                .ForMember(dest => dest.CantidadUnidadOrganica,
                        opt => opt.MapFrom(src => src.Usuario.UsuarioUnidadOrganicas.Count))
                .ForMember(dest => dest.MustChangePassword,
                        opt => opt.MapFrom(src => src.Usuario.MustChangePassword));
        }
    }
}
