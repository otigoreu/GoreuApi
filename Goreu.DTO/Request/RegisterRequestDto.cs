using System.ComponentModel.DataAnnotations;
namespace Goreu.Dto.Request
{
    public class RegisterRequestDto
    {
        public bool EsEdicion { get; set; }
        [Required]
        public string UserName { get; set; } = default!;

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress]
        public string Email { get; set; } = default!;

        public string Iniciales { get; set; } = default!;

        [Required]
        public int IdPersona { get; set; }

        public string? RolId { get; set; }

        public string Password { get; set; } = default!;

        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmPassword { get; set; } = default!;
    }
}
