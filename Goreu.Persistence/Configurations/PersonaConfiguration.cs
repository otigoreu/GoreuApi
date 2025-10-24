using Goreu.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Goreu.Entities.Pide;

namespace Goreu.Persistence.Configurations
{
    public class PersonaConfiguration : IEntityTypeConfiguration<Persona>
    {
        public void Configure(EntityTypeBuilder<Persona> builder)
        {
            //nombre de la tabla
            builder.ToTable(nameof(Persona), "Administrador");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nombres).HasMaxLength(50);
            builder.Property(x => x.ApellidoPat).HasMaxLength(50);
            builder.Property(x => x.ApellidoMat).HasMaxLength(50);

            builder.Property(x => x.FechaNac)
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.Email).HasMaxLength(50).IsUnicode(false);
            builder.Property(x => x.NroDoc).HasMaxLength(9).IsUnicode(false);

            //builder.HasQueryFilter(x => x.Estado);

            //relacion con TipoDocuemnto
            builder
               .HasOne(e => e.TipoDocumento)
               .WithMany(c => c.Personas)
               .HasForeignKey(x => x.IdTipoDoc);

            builder.HasOne(c => c.CredencialReniec)
                .WithOne(p => p.Persona)
                .HasForeignKey<CredencialReniec>(p=>p.PersonaID);

        }
    }
}
