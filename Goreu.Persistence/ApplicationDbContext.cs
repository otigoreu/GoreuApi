namespace Goreu.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<Usuario, Rol, string,
        IdentityUserClaim<string>,
        UsuarioRol,
        IdentityUserLogin<string>,
        IdentityRoleClaim<string>,
        IdentityUserToken<string>>
    {
        public DbSet<UsuarioInfo> UsuariosInfo { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Configuración Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aplica todas las configuraciones IEntityTypeConfiguration
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Entidades sin clave (solo lectura o vistas)
            modelBuilder.Entity<AplicacionInfo>().HasNoKey();
            modelBuilder.Entity<UnidadOrganicaInfo>().HasNoKey();
            modelBuilder.Entity<EntidadInfo>().HasNoKey();
            modelBuilder.Entity<MenuInfoRol>().HasNoKey();
            modelBuilder.Entity<RolInfo>().HasNoKey();
            modelBuilder.Entity<RolEntidadAplicacionInfo>().HasNoKey();

            // UsuarioInfo se mapea a una vista o consulta sin clave
            modelBuilder.Entity<UsuarioInfo>().HasNoKey().ToView("UsuarioInfoView");

            // Si deseas mapear la tabla de UsuarioRol manualmente, ya lo hace UsuarioRolConfiguration
            // pero si quisieras forzarlo aquí también podrías hacer:
            // modelBuilder.Entity<UsuarioRol>().ToTable("UsuarioRol", "Administrador");
        }
    }
}

//namespace Goreu.Persistence
//{
//    public class ApplicationDbContext : IdentityDbContext<Usuario, Rol, string,
//        IdentityUserClaim<string>,
//        UsuarioRol,
//        IdentityUserLogin<string>,
//        IdentityRoleClaim<string>,
//        IdentityUserToken<string>>
//    {
//        public DbSet<UsuarioInfo> UsuariosInfo { get; set; }

//        public ApplicationDbContext(DbContextOptions options) : base(options)
//        {

//        }

//        //FLUENT api
//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);

//            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

//            modelBuilder.Entity<AplicacionInfo>().HasNoKey();
//            modelBuilder.Entity<UnidadOrganicaInfo>().HasNoKey();
//            modelBuilder.Entity<EntidadInfo>().HasNoKey();
//            modelBuilder.Entity<MenuInfoRol>().HasNoKey();
//            modelBuilder.Entity<RolInfo>();
//            modelBuilder.Entity<RolEntidadAplicacionInfo>();

//            // UsuarioInfo no tiene clave (Keyless)
//            modelBuilder.Entity<UsuarioInfo>().HasNoKey().ToView("UsuarioInfoView"); // nombre ficticio
//            // Esto lo marca como "query type" sin clave primaria


//            //modelBuilder.Entity<IdentityUserRole<string>>(x => x.ToTable("UsuarioRol", "Administrador"));
//        }

//    }
//}
