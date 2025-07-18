using Goreu.Entities;
using Goreu.Entities.Pide;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goreu.Persistence.Configurations.Pide
{
    public class CredencialReniecConfiguration : IEntityTypeConfiguration<CredencialReniec>
    {
        public void Configure(EntityTypeBuilder<CredencialReniec> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable(nameof(CredencialReniec), "Administrador");
            builder.HasQueryFilter(x => x.Estado);

           
        }
    }
}
