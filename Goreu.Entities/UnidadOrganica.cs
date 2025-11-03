namespace Goreu.Entities
{
    public class UnidadOrganica : EntityBase
    {
        public string Descripcion { get; set; } = default!;
        public string? Abrev { get; set; } = default!;
        public int IdEntidad { get; set; }
        public Entidad Entidad { get; set; }

        public ICollection<UsuarioUnidadOrganica> UsuarioUnidadOrganicas { get; set; } = new List<UsuarioUnidadOrganica>();

        // Recursividad
        public int? IdDependencia { get; set; }  // Puede ser null para las unidades raíz
        public UnidadOrganica? Dependencia { get; set; }  // Referencia a la unidad padre

        public ICollection<UnidadOrganica> Hijos { get; set; } = new List<UnidadOrganica>();  // Hijas

    }
}
