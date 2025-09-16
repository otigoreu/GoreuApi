namespace Goreu.Entities
{
    public class UsuarioUnidadOrganica : EntityBase
    {
        public string IdUsuario { get; set; }
        public int IdUnidadOrganica { get; set; }

        // Control de vigencia
        public DateTime Desde { get; set; }
        public DateTime? Hasta { get; set; } // Nullable si todavía tiene acceso
        public DateTime? FechaAnulacion { get; set; }
        public string? ObservacionAnulacion { get; set; }

        public Usuario Usuario { get; set; }
        public UnidadOrganica UnidadOrganica { get; set; }
    }
}