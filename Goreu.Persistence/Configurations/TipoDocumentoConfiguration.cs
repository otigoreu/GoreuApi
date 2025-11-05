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
    public class TipoDocumentoConfiguration : IEntityTypeConfiguration<TipoDocumento>
    {
        public void Configure(EntityTypeBuilder<TipoDocumento> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Descripcion).HasMaxLength(200);
            builder.Property(x => x.Abrev).HasMaxLength(50);
            builder.ToTable(nameof(TipoDocumento), "adm");
            //builder.HasQueryFilter(x => x.Estado);
        }
    }
}
