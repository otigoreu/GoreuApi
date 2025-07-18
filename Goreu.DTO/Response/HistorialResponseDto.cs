using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Dto.Response
{
    public class HistorialResponseDto
    {
        public int Id { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string operacion { get; set; }
        public int ID_pk { get; set; }
        public bool Estado { get; set; } = true;

        public int idIndiceTabla { get; set; }
        public string idUsuario { get; set; }
    }
}
