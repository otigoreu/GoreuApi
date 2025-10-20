using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Goreu.Entities.Info
{
    public class RolEntidadAplicacionCounterInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public int IdEntidadAplicacion { get; set; }
        public int CantidadMenus { get; set; } = 0;
        public bool Estado { get; set; } = default!;
    }
}
