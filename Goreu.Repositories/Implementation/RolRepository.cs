using Goreu.Dto.Response;
using Goreu.Entities;
using Goreu.Entities.Info;
using Goreu.Persistence;
using Goreu.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Goreu.Repositories.Implementation
{
    public class RolRepository : IRolRepository
    {

        private readonly IHttpContextAccessor httpContext;
        private readonly DbContext context;

        public RolRepository(ApplicationDbContext context, IHttpContextAccessor httpContext)
        {
            this.httpContext = httpContext;
            this.context = context;
        }

        public async Task<Rol?> GetAsync(string id)
        {
            return await context.Set<Rol>().FindAsync(id);
        }


        public async Task<string> AddAsync(Rol rol)
        {
            await context.Set<Rol>().AddAsync(rol);
            await context.SaveChangesAsync();
            return rol.Id;
        }

        public async Task<ICollection<Rol>> GetAllAsync()
        {
            return await context.Set<Rol>().AsNoTracking().ToListAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var item = await context.Set<Rol>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (item is not null)
            {
                context.Set<Rol>().Remove(item);
                await context.SaveChangesAsync();
            }
            else
                throw new InvalidOperationException($"No se encontro el registro con id {id}");
        }

        public async Task FinalizedAsync(string id)
        {
            var rol = await GetAsync(id);
            if (rol is not null)
            {
                rol.Estado = false;
                await context.SaveChangesAsync();
            }
        }

        public async Task InitializedAsync(string id)
        {
            var rol = await context.Set<Rol>().IgnoreQueryFilters().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (rol is not null)
            {
                rol.Estado = true;
                context.Set<Rol>().Update(rol);
                await context.SaveChangesAsync();

            }
        }

        public async Task<ICollection<RolInfo>> GetAsyncPerUser(string idUser)
        {
            var query = context.Set<RolInfo>().FromSqlRaw(
                @"select r.Id, r.Name from Administrador.Rol r 
                join Administrador.UsuarioRol ur on r.Id=ur.RoleId 
                join Administrador.Usuario u on u.Id=ur.UserId where u.Id={0}", idUser);

            return await query.ToListAsync();
               
        }
    }
}
