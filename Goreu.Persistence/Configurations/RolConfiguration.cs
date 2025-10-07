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
            
            builder.ToTable(nameof(Rol), "Administrador");
            builder.Property(x => x.Name).IsUnicode(false);
            builder.Property(x => x.NormalizedName).IsUnicode(false);

        }
    }
}
