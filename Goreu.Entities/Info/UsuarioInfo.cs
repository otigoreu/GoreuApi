namespace Goreu.Entities.Info
{
    public class UsuarioInfo
    {
        public string Id { get; set; } = default!;
        
        public int IdPersona { get; set; }
        public string Email { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Nombres { get; set; }= default!;
        public string ApellidoPat { get; set; } = default!;
        public string ApellidoMat { get; set; } = default!;
        public string Entidad_Descripcion { get; set; } = default!;
        public string Aplicacion_Descripcion { get; set; } = default!;
        public string Rol_Descripcion { get; set; } = default!;
        public int CantidadUnidadOrganica { get; set; }
        public bool MustChangePassword { get; set; }
        public bool Estado { get; set; }
    }
}