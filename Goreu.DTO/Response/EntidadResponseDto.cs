namespace Goreu.Dto.Response
{
    public class EntidadResponseDto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = default!;
        public string Ruc { get; set; } = default!;
        public string Sigla { get; set; } = default!;
        public bool Estado { get; set; } = true;
        public int CantidadAplicaciones { get; set; }
    }
}
