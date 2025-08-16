namespace Goreu.Dto.Response
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = default!;
        public DateTime ExpirationDate { get; set; }
        public String IdUsuario { get; set; }
        public List<RolResponseSingleDto> Roles { get; set; }

        public EntidadResponseDto Entidad { get; set; } 
        public List<UnidadOrganicaResponseSingleDto> UnidadOrganicas { get; set; }
        public PersonaResponseDto Persona { get; set; }
        public List<AplicacionResponseDto> Aplicaciones { get; set; }
    }
}
