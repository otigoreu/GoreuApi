
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Goreu.Entities
{
    public class Rol : IdentityRole
    {
        public bool Estado { get; set; } = true;
        public ICollection<MenuRol> MenuRoles { get; set; }
        public ICollection<EntidadAplicacionRol> EntidadAplicacioneRoles { get; set; }

    }
}
