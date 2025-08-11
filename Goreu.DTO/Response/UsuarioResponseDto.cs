namespace Goreu.Dto.Response
{
    public class UsuarioResponseDto
    {
        public string Id { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public bool EsSuperUser { get; set; }
        public bool Estado { get; set; }
        public int CantidadUnidadorganicas { get; set; }
        public int CantidadRols { get; set; }
        public List<string> Roles { get; set; } = default!;

        public string descripcionPersona { get; set; }
    }
}
