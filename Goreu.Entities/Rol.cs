
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Goreu.Entities
{
    public class Rol : IdentityRole
    {
        public bool Estado { get; set; } = true;

        public int IdTipoRol { get; set; }

        [ForeignKey("IdTipoRol")]
        public TipoRol TipoRol { get; set; }
        public int IdEntidadAplicacion {get;set;}

        [ForeignKey("IdEntidadAplicacion")]
        public EntidadAplicacion EntidadAplicacion { get; set; }
        public ICollection<MenuRol> MenuRoles { get; set; }
        

    }
}
