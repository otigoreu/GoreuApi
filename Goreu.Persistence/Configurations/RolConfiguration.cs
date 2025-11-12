using Goreu.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goreu.Persistence.Configurations
{
    public class RolConfiguration : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.ToTable(nameof(Rol), "adm");

            builder.Property(x => x.Name).IsUnicode(false);
            builder.Property(x => x.NormalizedName).IsUnicode(false);

            // Eliminamos el índice único que viene por defecto en Identity (RoleNameIndex)
            builder.HasIndex(r => r.NormalizedName).IsUnique(false);

            // Creamos un índice único compuesto (IdEntidadAplicacion + NormalizedName)
            builder.HasIndex(r => new { r.IdEntidadAplicacion, r.NormalizedName })
                   .IsUnique()
                   .HasDatabaseName("IX_Rol_IdEntidadAplicacion_NormalizedName");

            // Relaciones (si las necesitás más adelante)
            // builder.HasMany(r => r.UsuarioRoles)
            //        .WithOne(ur => ur.Rol)
            //        .HasForeignKey(ur => ur.RoleId);
        }
    }
}