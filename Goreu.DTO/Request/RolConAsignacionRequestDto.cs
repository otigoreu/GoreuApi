namespace Goreu.Dto.Request
{
    public class RolConAsignacionRequestDto
    {
        public int IdEntidad { get; set; }
        public int IdAplicacion { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string RolId { get; set; } = string.Empty;
        public bool Selected { get; set; }
    }
}
