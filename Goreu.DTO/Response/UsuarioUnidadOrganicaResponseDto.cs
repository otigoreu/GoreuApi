namespace Goreu.Dto.Response
{
    public class UsuarioUnidadOrganicaResponseDto
    {
        public int Id { get; set; }
        public string IdUsuario { get; set; }
        public int IdUnidadOrganica { get; set; }
        public string Numdoc { get; set; }
        public string DescripcionPersona { get; set; }
        public bool Estado { get; set; }
    }
}
