using Goreu.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goreu.Persistence.Configurations
{
    public class EntidadAplicacionConfiguration : IEntityTypeConfiguration<EntidadAplicacion>
    {
        public void Configure(EntityTypeBuilder<EntidadAplicacion> builder)
        {

            builder.HasKey(x => x.Id);
            builder.ToTable(nameof(EntidadAplicacion), "Administrador");
            //configuracion de relacion con entidad
            builder.HasOne(ua => ua.Entidad)
                .WithMany(u => u.EntidadAplicaciones)
                .HasForeignKey(ua => ua.IdEntidad);
            //.OnDelete(DeleteBehavior.Cascade);

            //configuracion de relacion con aplicacion
            builder.HasOne(ua => ua.Aplicacion)
                .WithMany(a => a.EntidadAplicaciones)
                .HasForeignKey(ua => ua.IdAplicacion);
                //.OnDelete(DeleteBehavior.Restrict);
        }
    }
}
