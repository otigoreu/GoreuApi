using Goreu.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goreu.Persistence.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable(nameof(Usuario), "Administrador");
            builder.Property(x => x.Iniciales).HasMaxLength(7);

            builder.Property(x => x.UserName).IsUnicode(false);

            builder.Property(x => x.Email).IsUnicode(false);

            builder.Property(x => x.MustChangePassword).HasDefaultValue(false); // 👈 valor por defecto

            builder.HasOne(ua => ua.Persona)
                   .WithMany(u => u.Usuarios)
                   .HasForeignKey(ua => ua.IdPersona);
        }
    }
}
