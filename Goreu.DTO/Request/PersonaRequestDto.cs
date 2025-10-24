using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Dto.Request
{
    public class PersonaRequestDto
    {
        public string nombres { get; set; } = default!;
        public string apellidoPat { get; set; } = default!;
        public string apellidoMat { get; set; } = default!;
        public DateTime fechaNac { get; set; } = default!;
        public string email { get; set; } = default!;
        public int idTipoDoc { get; set; }
        public string nroDoc { get; set; } = default!;
        public bool Status { get; set; } = true;
    }
}
