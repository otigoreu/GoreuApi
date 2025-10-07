using System.ComponentModel.DataAnnotations;

namespace Goreu.Entities
{
    public class UsuarioRol : IdentityUserRole<string>
    {
        public bool Estado { get; set; } = true;
        public virtual Usuario? Usuario { get; set; }
        public virtual Rol? Rol { get; set; }
      
       

    }
}
