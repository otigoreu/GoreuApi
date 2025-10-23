using Goreu.Dto.Request;
using Goreu.Dto.Response;
using Goreu.Entities;
using Goreu.Entities.Info;
using Goreu.Persistence;
using Goreu.Repositories.Interface;
using Goreu.Repositories.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net.Http;

namespace Goreu.Repositories.Implementation
{
    public class AplicacionRepository : RepositoryBase<Aplicacion>, IAplicacionRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public AplicacionRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ICollection<Aplicacion>> GetAllbyEntidad<TKey>(int idEntidad, Expression<Func<Aplicacion, TKey>> orderBy, PaginationDto pagination)
        {
            var queryable = context.Set<Aplicacion>()
                .Where(app => app.EntidadAplicaciones.Any(ea => ea.IdEntidad==idEntidad))
                .OrderBy(orderBy)
                .AsNoTracking()
                .AsQueryable();
            await httpContextAccessor.HttpContext.InsertarPaginacionHeader(queryable);
            var response = await queryable.Paginate(pagination).ToListAsync();

            return response;
        }

        public async Task<ICollection<Aplicacion>> GetAsync<TKey>(Expression<Func<Aplicacion, bool>> predicate, Expression<Func<Aplicacion, TKey>> orderBy, PaginationDto pagination)
        {
            var queryable = context.Set<Aplicacion>()

                .Where(predicate)
                .OrderBy(orderBy)
                .AsNoTracking()
                .AsQueryable();

            await httpContextAccessor.HttpContext.InsertarPaginacionHeader(queryable);
            var response = await queryable.Paginate(pagination).ToListAsync();

            return response;
        }

        public async Task<AplicacionInfo> GetAsyncPerRol(string idRol)
        {
            var query = context.Set<AplicacionInfo>().FromSqlRaw(
                @"select a.id, a.Descripcion, a.Estado from Administrador.Aplicacion a 
                join Administrador.EntidadAplicacion ea on a.Id=ea.IdAplicacion 
                join Administrador.Rol r on r.IdEntidadAplicacion=ea.IdAplicacion where r.id={0}", idRol);

            return await query.SingleOrDefaultAsync();
        }

        public async Task<ICollection<AplicacionInfo>> GetAsyncPerUser(string idUser)
        {
            
            var query = context.Set<AplicacionInfo>().FromSqlRaw(
                @"select distinct  a.Id, a.Descripcion, a.Estado from Administrador.Usuario u 
                join Administrador.UsuarioRol ur on u.Id=ur.UserId join Administrador.Rol r on r.Id=ur.RoleId
                join Administrador.EntidadAplicacion ea on r.IdEntidadAplicacion=ea.Id join Administrador.Aplicacion a on a.Id=ea.IdAplicacion 
                where u.Id={0}", idUser);

            return await query.ToListAsync();
        }
    }
}
