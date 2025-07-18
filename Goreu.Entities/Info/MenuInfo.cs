using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Entities.Info
{
    public class MenuInfo
    {
        public int Id { get; set; }
        public string Descripcion{ get; set; } = default!;
        public string Icono { get; set; } = default!;
        public string Ruta { get; set; } = default!;
        public int IdAplicacion { get; set; }
        public string Aplicacion { get; set; } = default!;
        public int? IdMenuPadre { get; set; }
        public bool Estado { get; set; } = default!;
    }
}
