namespace Goreu.Dto.Response
{
    public class UsuarioUnidadOrganica_UnidadOrganicaResponseDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int IdUnidadOrganica { get; set; }
        public string Descripcion_UnidadOrganica { get; set; }
        public DateTime? Desde { get; set; }
        public DateTime? Hasta { get; set; }
        public bool Estado { get; set; }
    }
}
