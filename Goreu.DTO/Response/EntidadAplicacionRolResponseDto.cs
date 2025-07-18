using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Dto.Response
{
    public class EntidadAplicacionRolResponseDto
    {
        public int Id { get; set; }
        public int IdEntidadAplicacionRol { get; set; }
        public string IdRol { get; set; }
        public bool Estado { get; set; } = true;
    }
}
