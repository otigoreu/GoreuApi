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
        public string Abrev { get; set; } 
        public string? NombreEntidad { get; set; }
        public string? NombreDependencia { get; set; }
        public int CantidadHijos { get; set; }
        public bool Estado { get; set; }
        public int CantidadUsuarios { get; set; }

        public int idEntidad { get; set; }
        public int? idDependencia { get; set; }
    }
}
