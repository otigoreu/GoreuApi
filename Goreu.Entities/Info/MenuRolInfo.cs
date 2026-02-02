namespace Goreu.Entities.Info
{
    public class MenuRolInfo
    {
        public int Id { get; set; }
        public bool Operacion { get; set; }
        public bool Consulta { get; set; }
        public int IdMenu { get; set; }
        public int? IdMenuPadre { get; set; }
        public string DescripcionMenu { get; set; }
        public string IconoMenu { get; set; } 
        public string IdRol { get; set; }
        public bool Estado { get; set; }
    }
}
