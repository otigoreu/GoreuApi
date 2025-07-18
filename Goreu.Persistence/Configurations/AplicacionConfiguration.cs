using Goreu.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Persistence.Configurations
{
    class AplicacionConfiguration : IEntityTypeConfiguration<Aplicacion>
    {
        public void Configure(EntityTypeBuilder<Aplicacion> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Descripcion).HasMaxLength(50);
            builder.ToTable(nameof(Aplicacion), "Administrador");
            //builder.HasQueryFilter(x => x.Estado);
        }
    }
}
