using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Entities.Info
{
    public class PersonaInfo
    {
        public int Id { get; set; }
        public string nombres { get; set; } = default!;
        public string apellidoPat { get; set; } = default!;
        public string apellidoMat { get; set; } = default!;

        public DateTime fechaNac { get; set; } = default!;
        public int edad { get; set; }
        public string email { get; set; } = default!;
        public int idTipoDoc { get; set; }
        public string nroDoc { get; set; } = default!;
        public bool estado { get; set; } = default!;
    }
}
