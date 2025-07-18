using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Dto.Response.Pide.Integraciones
{
    public class ReniecResponseDto
    {
        public string Nombre { get; set; }
        public string ApePaterno { get; set; }
        public string ApeMaterno { get; set; }
        public string Direccion { get; set; }
        public string EstadoCivil { get; set; }
        public string Restriccion { get; set; }
        public string Ubigeo { get; set; }
        public string Foto { get; set; }
    }
}
