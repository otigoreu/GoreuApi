using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Entities
{
    public class Entidad :EntityBase
    {
        public string Descripcion { get; set; } = default!;
        public string Ruc { get; set; } = default!;
        public string Sigla { get; set; } = default!;
        public ICollection<UnidadOrganica> UnidadOrganicas { get; set; } = default!;
        public ICollection<EntidadAplicacion> EntidadAplicaciones { get; set; }
    }
}
