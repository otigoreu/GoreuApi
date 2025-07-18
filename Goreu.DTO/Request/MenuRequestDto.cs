using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Dto.Request
{
    public class MenuRequestDto
    {
        public string Descripcion { get; set; } = default!;
        public string Icono { get; set; } = default!;
        public string Ruta { get; set; } = default!;
        public int IdAplicacion { get; set; }
        public List<string> IdRoles { get; set; } = default!;
        public int? IdMenuPadre { get; set; }
    }
}
