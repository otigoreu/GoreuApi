namespace Goreu.Dto.Response
{
    public class UnidadOrganicaTreeResponseDto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = default!;
        public string? Abrev { get; set; }
        public int IdEntidad { get; set; }
        public int? IdDependencia { get; set; }  // Puede ser null para las unidades raíz
        public List<UnidadOrganicaTreeResponseDto> Hijos { get; set; } = new();
    }
}
