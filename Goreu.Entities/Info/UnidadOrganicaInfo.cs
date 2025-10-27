using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Entities.Info
{
    public class UnidadOrganicaInfo
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = default!;
        public string Abrev { get; set; } = default!;
        public int IdEntidad { get; set; }
        public bool Estado { get; set; } = default!;

    }
}
