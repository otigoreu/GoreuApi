namespace Goreu.Dto.Request
{
    public class UsuarioUnidadOrganicaRequestDto
    {
        public string IdUsuario { get; set; }
        public int IdUnidadOrganica { get; set; }
        public DateTime Desde { get; set; }
        public DateTime? Hasta{ get; set; }
        public bool Estado { get; set; } = true;
    }
}
