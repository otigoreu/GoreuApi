using Goreu.Entities;
using Goreu.Entities.Info;
using Goreu.Persistence;
using Goreu.Repositories.Interface;
using Goreu.Repositories.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Repositories.Implementation
{
    public class MenuRepository : RepositoryBase<Menu>, IMenuRepository
    {
        private readonly IHttpContextAccessor httpContext;
        public MenuRepository(ApplicationDbContext context, IHttpContextAccessor httpContext) : base(context)
        {
            this.httpContext = httpContext;
        }


        public async Task<ICollection<Menu>> GetByIdAplicationAsync(int idAplication)
        {
            return await context.Set<Menu>().Include(x => x.MenuRoles)
               .Where(x => x.IdAplicacion == idAplication)
               .ToListAsync();
        }

        public async Task<List<Menu>> GetMenusByApplicationAndRolesAsync(int applicationId, List<string> roleIds)
        {
            return await context.Set<Menu>()
               .Where(menu => menu.IdAplicacion == applicationId &&
                              menu.MenuRoles.Any(mr => roleIds.Contains(mr.IdRol)))
               .ToListAsync();
        }


        public async Task<ICollection<MenuInfo>> GetAsync(string? Descripcion)
        {
            var queryable = context.Set<Menu>()
               .Include(x => x.Aplicacion)
               .Where(x => x.Descripcion.Contains(Descripcion ?? string.Empty))
               .IgnoreQueryFilters()
               .AsNoTracking()
               .Select(x => new MenuInfo
               {
                   Id = x.Id,
                   Descripcion = x.Descripcion,
                   Icono = x.Icono,
                   Ruta = x.Ruta,
                   IdAplicacion = x.Aplicacion.Id,
                   Aplicacion = x.Aplicacion.Descripcion,
                   IdMenuPadre = x.IdMenuPadre,
                   Estado = x.Estado

               }).AsQueryable();

            await httpContext.HttpContext.InsertarPaginacionHeader(queryable);
            return await queryable.ToListAsync();
        }


        public async Task InitializedAsync(int id)
        {
            var menu = await context.Set<Menu>().IgnoreQueryFilters().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (menu is not null)
            {
                menu.Estado = true;
                context.Set<Menu>().Update(menu);
                await context.SaveChangesAsync();

            }
        }
        public async Task FinalizedAsync(int id)
        {
            var menu = await GetAsync(id);
            if (menu is not null)
            {
                menu.Estado= false;
                await UpdateAsync();
            }
        }

        public async Task<ICollection<MenuInfoRol>> GetAsyncWithRole(string? Descripcion)
        {

            //var query = context.Set<MenuInfoRol>().FromSqlRaw("MenuWithRolAndApp {0}", Descripcion ?? string.Empty);

            var query = context.Set<MenuInfoRol>().FromSqlRaw(
                @"select 
	                m.Id, 
	                m.Descripcion,
	                m.Icono, 
	                m.Ruta,
	                a.Id as IdAplicacion, 
	                a.Descripcion as Aplicacion , 
	                r.Id as IdRol, 
	                r.Name as Rol, 
	                m.IdMenuPadre,
	                m.Estado 
	                from Administrador.Menu m 
			                join Administrador.MenuRol mr on m.Id=mr.IdMenu 
			                join Administrador.Rol r on r.Id=mr.IdRol 
			                join Administrador.Aplicacion a on m.IdAplicacion=a.Id
		                where (m.Descripcion like '%'+{0}+'%')", Descripcion ?? string.Empty );

            return await query.ToListAsync();

        }
    }
}
