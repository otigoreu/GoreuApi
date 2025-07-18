using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Entities.Info
{
    public class MenuInfoRol
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = default!;
        public string Icono { get; set; } = default!;
        public string Ruta { get; set; } = default!;

        public int IdAplicacion { get; set; }
        public string Aplicacion { get; set; } = default!;
        public string IdRol { get; set; }
        public string Rol { get; set; } = default!;

        public bool Estado { get; set; } = default!;
        public int? IdMenuPadre { get; set; }
    }
}
