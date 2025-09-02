namespace Goreu.Entities
{
    public class Rol : IdentityRole
    {
        public bool Estado { get; set; } = true;
        
        public char Nivel { get; set; } = default!;
        public int IdEntidadAplicacion {get;set;}

        [ForeignKey("IdEntidadAplicacion")]
        public EntidadAplicacion EntidadAplicacion { get; set; }
        public ICollection<MenuRol> MenuRoles { get; set; }
    }
}
