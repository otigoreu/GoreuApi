namespace Goreu.Dto.Response
{
    public class UsuarioRol_UsuarioResponseDto
    {
        //public string UserId { get; set; } = default!;
        //public string RoleId { get; set; } = default!;
        public Guid Id { get; set; }
        public bool Estado { get; set; }

        public string UserName { get; set; } = default!;
        //public string Rol_Descripcion { get; set; }
        public string NombreCompleto { get; set; }
        public int CantidadUnidadOrganica { get; set; } = 0;
        public int CantidadRol { get; set; } = 0;
        public bool MustChangePassword { get; set; }
    }
}
