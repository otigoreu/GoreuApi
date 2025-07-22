using Goreu.Dto.Request;
using Goreu.Entities;
using Goreu.Entities.Info;
using Goreu.Persistence;
using Goreu.Repositories.Interface;
using Goreu.Repositories.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Goreu.Repositories.Implementation
{
    public class EntidadRepository : RepositoryBase<Entidad>, IEntidadRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public EntidadRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ICollection<Entidad>> GetAsync<TKey>(Expression<Func<Entidad, bool>> predicate, Expression<Func<Entidad, TKey>> orderBy, PaginationDto pagination)
        {
            var queryable = context.Set<Entidad>()
                .Include(z => z.EntidadAplicaciones.Where(ea => ea.Estado))
                .Where(predicate)
                .OrderBy(orderBy)
                .AsQueryable();

            await httpContextAccessor.HttpContext.InsertarPaginacionHeader(queryable);
            var response = await queryable.Paginate(pagination).ToListAsync();

            return response;
        }

        public async Task <EntidadInfo> GetAsyncPerUser(string idUser)
        {
            var query = context.Set<EntidadInfo>().FromSqlRaw(
                @"select distinct e.Id, e.Descripcion,e.Ruc,e.Estado from Administrador.UnidadOrganica uo 
                join Administrador.UsuarioUnidadOrganica uuo on uuo.IdUnidadOrganica= uo.Id join Administrador.Entidad e on e.Id=uo.IdEntidad
                join Administrador.Usuario u on u.Id=uuo.IdUsuario where u.Id={0}", idUser);

            //return await query.ElementAtAsync(0);
            return await query.SingleOrDefaultAsync();
        }
    }
}
