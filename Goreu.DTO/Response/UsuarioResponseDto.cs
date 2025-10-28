namespace Goreu.Dto.Response
{
    public class UsuarioResponseDto
    {
        public string Id { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string UserName { get; set; } = default!;

        public int IdPersona { get; set; }
        public string Iniciales { get; set; }
        public bool Estado { get; set; } = true;
        public bool MustChangePassword { get; set; }

        public int CantidadUnidadOrganica { get; set; }
        public string DescripcionPersona { get; set; }

        //public string Entidad_Descripcion { get; set; }
        //public string Aplicacion_Descripcion { get; set; }
        //public string Rol_Descripcion { get; set; }
    }
}
