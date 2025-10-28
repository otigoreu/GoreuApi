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
    public class HistorialConfiguration : IEntityTypeConfiguration<Historial>
    {
        public void Configure(EntityTypeBuilder<Historial> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable(nameof(Historial), "Seguridad");

            builder
              .HasOne(ua => ua.IndiceTabla)
              .WithMany(u => u.Historials)
              .HasForeignKey(ua => ua.idIndiceTabla);

            builder
              .HasOne(ua => ua.Usuario)
              .WithMany(u => u.Historiales)
              .HasForeignKey(ua => ua.idUsuario);
        }
    }
}
