using System.ComponentModel.DataAnnotations;

namespace Goreu.Entities
{
    public class UsuarioRol : IdentityUserRole<string>
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // 🔹 Clave primaria propia
        public new string UserId { get; set; } = string.Empty; // 🔹 Oculta la heredada, evita nulls
        public new string RoleId { get; set; } = string.Empty; // 🔹 Oculta la heredada, evita nulls

        public bool Estado { get; set; } = true;

        public virtual Usuario? Usuario { get; set; }
        public virtual Rol? Rol { get; set; }
    }
}
