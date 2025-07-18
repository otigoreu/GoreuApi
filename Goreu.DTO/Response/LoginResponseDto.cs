namespace Goreu.Dto.Response
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = default!;
        public DateTime ExpirationDate { get; set; }
        public List<string> Roles { get; set; } = default!;

        public EntidadResponseDto Entidad { get; set; } 
        public List<UnidadOrganicaResponseSingleDto> UnidadOrganicas { get; set; }
        public PersonaResponseDto Persona { get; set; }
        public List<AplicacionResponseDto> Aplicaciones { get; set; }
    }
}
