using Goreu.Entities;
using Goreu.Entities.Info;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Goreu.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<Usuario,Rol,string,
        IdentityUserClaim<string>,
        UsuarioRol,
        IdentityUserLogin<string>,
        IdentityRoleClaim<string>,
        IdentityUserToken<string>>
    {
        public DbSet<UsuarioInfo> UsuariosInfo { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        //FLUENT api
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<AplicacionInfo>().HasNoKey();
            modelBuilder.Entity<UnidadOrganicaInfo>().HasNoKey();
            modelBuilder.Entity<EntidadInfo>().HasNoKey();
            modelBuilder.Entity<MenuInfoRol>().HasNoKey();
            modelBuilder.Entity<RolInfo>();

            // UsuarioInfo no tiene clave (Keyless)
            modelBuilder.Entity<UsuarioInfo>().HasNoKey().ToView("UsuarioInfoView"); // nombre ficticio
            // Esto lo marca como "query type" sin clave primaria


            //modelBuilder.Entity<IdentityUserRole<string>>(x => x.ToTable("UsuarioRol", "Administrador"));
        }

    }
}
