using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Entities
{
    public class Menu :EntityBase
    {
        public string Descripcion { get; set; } = default!;
        public string Icono { get; set; } = default!;
        public string Ruta { get; set; } = default!;

        public int IdAplicacion { get; set; }
        public Aplicacion Aplicacion { get; set; }

        //auto referencia
        public int? IdMenuPadre { get; set; }
        public Menu MenuPadre { get; set; }

        // Colección de menús hijos
        public ICollection<Menu> MenuHijos { get; set; }

        public ICollection<MenuRol> MenuRoles { get; set; }

    }
}
