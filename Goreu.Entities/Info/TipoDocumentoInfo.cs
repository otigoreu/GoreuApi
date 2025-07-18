using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Entities.Info
{
    public class TipoDocumentoInfo
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = default!;
        public string Abrev { get; set; } = default!;
        public bool Estado { get; set; } = default!;
    }
}
