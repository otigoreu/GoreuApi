using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Dto.Response
{
    public class UnidadOrganicaResponseDto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string? NombreEntidad { get; set; }
        public string? NombreDependencia { get; set; }
        public int CantidadHijos { get; set; }
    }
}
