namespace Goreu.Entities
{
    public class Usuario : IdentityUser
    {
        public int IdPersona { get; set; }
        public string Iniciales { get; set; }
        public bool Estado { get; set; } = true;
        public bool MustChangePassword { get; set; }
        public Persona Persona {get; set;}

        public virtual ICollection<UsuarioUnidadOrganica> UsuarioUnidadOrganicas { get; set; } = new List<UsuarioUnidadOrganica>();
        public virtual ICollection<UsuarioRol> UsuarioRoles { get; set; } = new List<UsuarioRol>();
        public virtual ICollection<Historial> Historiales { get; set; } = new List<Historial>();
        //public ICollection<IdentityUserRole<string>> UsuarioRoles { get; set; }
    }
}
