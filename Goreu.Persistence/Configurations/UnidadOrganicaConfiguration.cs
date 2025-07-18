using Goreu.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Persistence.Configurations
{
    public class UnidadOrganicaConfiguration : IEntityTypeConfiguration<UnidadOrganica>
    {
        public void Configure(EntityTypeBuilder<UnidadOrganica> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Descripcion).HasMaxLength(50);
            builder.ToTable(nameof(UnidadOrganica), "Administrador");
            //builder.HasQueryFilter(x => x.Estado);

            builder.HasOne(ua => ua.Entidad)
                .WithMany(u => u.UnidadOrganicas)
                .HasForeignKey(ua => ua.IdEntidad);

            // Relación recursiva: Padre -> Hijas
            builder
                .HasOne(d => d.Dependencia)
                .WithMany(d => d.Hijos)
                .HasForeignKey(d => d.IdDependencia)
                .OnDelete(DeleteBehavior.Restrict); // Para evitar eliminación en cascada recursiva
        }
    }
}
