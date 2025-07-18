using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Entities
{
    public class EntidadAplicacion :EntityBase
    {
        public int IdEntidad { get; set; }
        public Entidad Entidad { get; set; }
        public int IdAplicacion { get; set; }
        public Aplicacion Aplicacion { get; set; }
        public ICollection<EntidadAplicacionRol> EntidadAplicacioneRoles { get; set; }
    }
}
