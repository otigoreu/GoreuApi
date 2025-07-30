using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Dto.Request
{
    public class RegisterRequestDto
    {
        [Required]
        public string UserName { get; set; } = default!;

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        public int IdPersona { get; set; }
        
        [Required]
        public int IdUnidadOrganica{ get; set; }

        [Required]
        public string Rol { get; set; }

        public string Password { get; set; } = default!;

        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmPassword { get; set; } = default!;
    }
}
