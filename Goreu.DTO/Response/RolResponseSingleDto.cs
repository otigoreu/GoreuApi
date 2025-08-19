using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Dto.Response
{
    public class RolResponseSingleDto
    {
        public string Id { get; set; } = default!;
        public char Nivel { get; set; } = default!;
        public string Name { get; set; } = default!;
    }
}
