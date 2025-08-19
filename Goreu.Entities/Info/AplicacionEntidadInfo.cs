using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Entities.Info
{
    public class AplicacionEntidadInfo
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = default!;
        public string Nivel{ get; set; }=default!;
        public string Entidad{ get; set; } = default!;
        public bool Estado { get; set; } = default!;
    }
}
