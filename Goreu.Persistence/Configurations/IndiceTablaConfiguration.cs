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
    public class IndiceTablaConfiguration : IEntityTypeConfiguration<IndiceTabla>
    {
        public void Configure(EntityTypeBuilder<IndiceTabla> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Descripcion).HasMaxLength(50);
            builder.ToTable(nameof(IndiceTabla), "Seguridad");
        }
    }
}
