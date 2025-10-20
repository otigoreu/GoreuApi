namespace Goreu.Dto.Response
{
    public class MenuResponseDto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = default!;
        public string Icono { get; set; } = default!;
        public string Ruta { get; set; } = default!;
        public int IdAplicacion { get; set; }
        public int? IdMenuPadre { get; set; }
        public bool Estado { get; set; } 
    }
}
