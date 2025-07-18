using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Entities
{
    public class EntidadAplicacionRol : EntityBase
    {
        public int IdEntidadAplicacion { get; set; }
        public EntidadAplicacion EntidadAplicacion { get;set; }
        public string IdRol { get; set; }
        public Rol Rol { get; set; }

    }
}
