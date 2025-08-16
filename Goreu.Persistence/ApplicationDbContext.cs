using Goreu.Entities;
using Goreu.Entities.Info;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Goreu.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<Usuario>
    {

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
            

            modelBuilder.Entity<IdentityUserRole<string>>(x => x.ToTable("UsuarioRol", "Administrador"));
        }

    }
}
