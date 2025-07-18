using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Entities
{
    public class Aplicacion :EntityBase
    {
        public string Descripcion { get; set; } = default!;
        public ICollection<Menu> Menus { get; set; } = default!;
        public ICollection<EntidadAplicacion> EntidadAplicaciones { get; set; }
    }
}
