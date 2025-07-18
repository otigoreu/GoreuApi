

using Microsoft.AspNetCore.Identity;

namespace Goreu.Entities
{
    public class Usuario : IdentityUser
    {
        
        public int IdPersona { get; set; }
        public Persona Persona {get; set;}
        public ICollection<UsuarioUnidadOrganica> UsuarioUnidadOrganicas { get; set; }
        public ICollection<Historial> Historials { get; set; } = new List<Historial>();

    }
}
