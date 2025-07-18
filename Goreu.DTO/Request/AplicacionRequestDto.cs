using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Dto.Request
{
    public class AplicacionRequestDto
    {
        public string Descripcion { get; set; } = default!;
        public List<int> idMenus { get; set; }
    }
}
