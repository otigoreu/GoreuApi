namespace Goreu.Persistence.Configurations
{
    public class UsuarioRolConfiguration : IEntityTypeConfiguration<UsuarioRol>
    {
        public void Configure(EntityTypeBuilder<UsuarioRol> builder)
        {
            builder.ToTable(nameof(UsuarioRol), "Administrador");

            // 🔹 Nueva clave primaria
            builder.HasKey(ur => ur.Id);

            // 🔹 Generación automática del GUID
            builder.Property(ur => ur.Id)
                   .HasDefaultValueSql("NEWID()")
                   .IsRequired();

            builder.Property(ur => ur.Estado)
                   .HasDefaultValue(true);

            // 🔹 Relaciones
            builder.HasOne(ur => ur.Usuario)
                   .WithMany()
                   .HasForeignKey(ur => ur.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ur => ur.Rol)
                   .WithMany()
                   .HasForeignKey(ur => ur.RoleId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}


//namespace Goreu.Persistence.Configurations
//{
//    public class UsuarioRolConfiguration : IEntityTypeConfiguration<UsuarioRol>
//    {
//        public void Configure(EntityTypeBuilder<UsuarioRol> builder)
//        {
//            //builder.ToTable(nameof(UsuarioRol), "Administrador");
//            builder.ToTable(nameof(UsuarioRol), "Administrador");

//            builder.HasKey(ur => new { ur.UserId, ur.RoleId });

//            builder.Property(x => x.Estado);

//            builder.HasOne(ur => ur.Usuario)
//                    .WithMany()
//                    .HasForeignKey(ur => ur.UserId)
//                    .IsRequired();

//            builder.HasOne(ur => ur.Rol)
//                .WithMany()
//                .HasForeignKey(ur => ur.RoleId)
//                .IsRequired();

//        }
//    }
//}
