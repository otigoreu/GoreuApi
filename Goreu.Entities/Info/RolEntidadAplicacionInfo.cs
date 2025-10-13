using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Goreu.Entities.Info
{
    public class RolEntidadAplicacionInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public int IdEntidadAplicacion { get; set; }
        public string EntidadAplicacion { get; set; }
        public bool Estado { get; set; } = default!;
    }
}
