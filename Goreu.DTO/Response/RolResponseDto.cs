namespace Goreu.DtoResponse
{
    public class RolResponseDto
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string NormalizedName { get; set; } = default!;
        public bool Estado { get; set; } = default!;

        public char Nivel { get; set; }
        public int idEntidadAplicacion { get; set; }
    }
}
