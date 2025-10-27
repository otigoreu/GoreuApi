namespace Goreu.Dto.Request
{
    public class UnidadOrganicaRequestDto
    {
        public string Descripcion { get; set; } = default!;
        public string Abrev { get; set; } = default!;
        public int IdEntidad { get; set; }
        public int? IdDependencia { get; set; }
    }
}
