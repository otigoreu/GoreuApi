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
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Descripcion).HasMaxLength(50);
            builder.Property(x => x.Icono).HasMaxLength(50);
            builder.Property(x => x.Ruta).HasMaxLength(200);
            builder.ToTable(nameof(Menu), "Administrador");
            //builder.HasQueryFilter(x => x.Estado);

            //// Configurar la relación autorreferente
            builder.HasOne(m => m.MenuPadre)          // Un menú tiene un menú padre
                   .WithMany(m => m.MenuHijos)          // Un menú puede tener muchos menús hijos
                   .HasForeignKey(m => m.IdMenuPadre) // Clave foránea en ParentMenuId
                   .OnDelete(DeleteBehavior.Restrict); // Opcional: Evitar eliminación en cascada
                                                       // Configurar la relación con AspNetUsers
            builder.HasOne(ua => ua.Aplicacion)
                   .WithMany(u => u.Menus)
                   .HasForeignKey(ua => ua.IdAplicacion);
        }
    }
}
