
using System.ComponentModel.DataAnnotations;

namespace Goreu.Dto.Request
{
    public class UsuarioRequestDto
    {
        public string Email { get; set; } = default!;
        public string Iniciales { get; set; } = default!;
        public string nombres { get; set; } = default!;
        public string apellidoPat { get; set; } = default!;
        public string apellidoMat { get; set; } = default!;
        public string nroDoc { get; set; } = default!;

    }
}
