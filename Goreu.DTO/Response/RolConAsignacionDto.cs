namespace Goreu.Dto.Response
{
    public class RolConAsignacionDto
    {
        public string Id { get; set; } = default!;
        public string Descripcion { get; set; } = default!;
        public bool Asignado { get; set; }
    }
}
