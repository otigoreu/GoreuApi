namespace Goreu.Dto.Response
{
    public class UsuarioUnidadOrganicaResponseDto
    {
        public int Id { get; set; }
        public string IdUsuario { get; set; }
        public int IdUnidadOrganica { get; set; }
        public DateTime desde { get; set; }
        public DateTime? hasta { get; set; } = null;
        public string Numdoc { get; set; }
        public string DescripcionPersona { get; set; }
        public bool Estado { get; set; }
    }
}
