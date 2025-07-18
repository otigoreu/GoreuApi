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
    public class UsuarioUnidadOrganicaConfiguration : IEntityTypeConfiguration<UsuarioUnidadOrganica>
    {
        public void Configure(EntityTypeBuilder<UsuarioUnidadOrganica> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable(nameof(UsuarioUnidadOrganica), "Administrador");

            //configurar la relacion con usuario
            builder.HasOne(ua => ua.Usuario)
                .WithMany(u => u.UsuarioUnidadOrganicas)
                .HasForeignKey(ua => ua.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            //configurar la relacion con UnidadOrganica
            builder.HasOne(ua => ua.UnidadOrganica)
                .WithMany(a => a.UsuarioUnidadOrganicas)
                .HasForeignKey(ua => ua.IdUnidadOrganica)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
