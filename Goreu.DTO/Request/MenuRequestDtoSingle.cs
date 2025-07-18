using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Dto.Request
{
    public class MenuRequestDtoSingle
    {
        public string Descripcion { get; set; } = default!;
        public string Icono { get; set; } = default!;
        public string Ruta { get; set; } = default!;
        public int IdAplicacion { get; set; }
        public int? IdMenuPadre { get; set; }
    }
}
