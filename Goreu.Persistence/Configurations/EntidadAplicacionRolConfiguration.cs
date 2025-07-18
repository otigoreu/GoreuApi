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
    public class EntidadAplicacionRolConfiguration : IEntityTypeConfiguration<EntidadAplicacionRol>
    {
        public void Configure(EntityTypeBuilder<EntidadAplicacionRol> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable(nameof(EntidadAplicacionRol), "Administrador");
            //relacion con rol
            builder.HasOne(ua => ua.Rol)
                .WithMany(u => u.EntidadAplicacioneRoles)
                .HasForeignKey(ua => ua.IdRol);

            //relacion con EntidadAplicacion
            builder.HasOne(ua => ua.EntidadAplicacion)
                .WithMany(u=>u.EntidadAplicacioneRoles)
                .HasForeignKey(ua=>ua.IdEntidadAplicacion);
        }
    }
}
