using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.DtoResponse
{
    public class RolResponseDto
    {
        public string Id { get; set; } = default!;
        public char Nivel { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string NormalizedName { get; set; } = default!;
        public bool Estado { get; set; } = default!;
    }
}
