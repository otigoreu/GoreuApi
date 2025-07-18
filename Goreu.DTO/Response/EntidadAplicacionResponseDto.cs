namespace Goreu.Dto.Response
{
    public class EntidadAplicacionResponseDto
    {
        public int Id { get; set; }
        public int IdEntidad { get; set; }
        public int IdAplicacion { get; set; }
        public string DescripcionAplicacion { get; set; }
        public bool Estado { get; set; } = true;
    }
}
