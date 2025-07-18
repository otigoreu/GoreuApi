using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Entities
{
    public class TipoDocumento :EntityBase
    {
        public string Descripcion { get; set; } = default!;
        public string Abrev { get; set; } = default!;

        public ICollection<Persona> Personas { get; private set; }
    }
}
