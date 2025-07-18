using System.ComponentModel.DataAnnotations;

namespace Goreu.Dto.Request
{
    public class NewPasswordRequestDto
    {
        public string Email { get; set; } = default!;
        public string Token { get; set; } = default!;
        public string NewPassword { get; set; } = default!;
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; } = default!;
    }
}
