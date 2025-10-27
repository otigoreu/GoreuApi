namespace Goreu.Entities
{
    public class Usuario : IdentityUser
    {
        public int IdPersona { get; set; }
        public string Iniciales { get; set; }
        public bool Estado { get; set; } = true;
        public bool MustChangePassword { get; set; }
        public Persona Persona {get; set;}
        public ICollection<UsuarioUnidadOrganica> UsuarioUnidadOrganicas { get; set; }
        //public ICollection<UsuarioRol> UsuarioRols { get; set; }
        public ICollection<Historial> Historials { get; set; } = new List<Historial>();
        //public ICollection<IdentityUserRole<string>> UsuarioRoles { get; set; }
    }
}
