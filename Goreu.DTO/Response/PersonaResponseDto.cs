using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Dto.Response
{
    public class PersonaResponseDto
    {
        public int Id { get; set; }
        public string Nombres { get; set; } = default!;
        public string ApellidoPat { get; set; } = default!;
        public string ApellidoMat { get; set; } = default!;
        public DateTime FechaNac { get; set; }
        public string Email { get; set; } = default!;
        public int IdTipoDoc { get; set; }
        public string NroDoc { get; set; } = default!;
        public string NombreCompleto { get; set; } = default!;
    }
}
