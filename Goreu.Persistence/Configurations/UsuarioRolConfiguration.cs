using Goreu.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goreu.Persistence.Configurations
{
    public class UsuarioRolConfiguration : IEntityTypeConfiguration<UsuarioRol>
    {
        public void Configure(EntityTypeBuilder<UsuarioRol> builder)
        {
            //builder.ToTable(nameof(UsuarioRol), "Administrador");
            builder.ToTable(nameof(UsuarioRol), "Administrador");
            builder.HasKey(ur => new { ur.UserId, ur.RoleId });
            builder.Property(x => x.Estado);
            builder.HasOne(ur => ur.Usuario)
                    .WithMany()
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

            builder.HasOne(ur => ur.Rol)
                .WithMany()
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

        }
    }
}
