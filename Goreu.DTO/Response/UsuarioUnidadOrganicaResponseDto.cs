using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Dto.Response
{
    public class UsuarioUnidadOrganicaResponseDto
    {
        public int Id { get; set; }
        public string IdUsuario { get; set; }
        public int IdunidadOrganica { get; set; }
        public bool Estado { get; set; }
    }
}
