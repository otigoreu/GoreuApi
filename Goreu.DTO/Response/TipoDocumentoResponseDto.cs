using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Dto.Response
{
    public class TipoDocumentoResponseDto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = default!;
        public string Abrev { get; set; } = default!;
        public string Estado { get; set; } = default!;
    }
}
