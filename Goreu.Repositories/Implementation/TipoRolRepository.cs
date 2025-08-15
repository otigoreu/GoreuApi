using Goreu.Dto.Request;
using Goreu.Entities;
using Goreu.Persistence;
using Goreu.Repositories.Interface;
using Goreu.Repositories.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Goreu.Repositories.Implementation
{
    public class TipoRolRepository : RepositoryBase<TipoRol>, ITipoRolRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public TipoRolRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ICollection<TipoRol>> GetAsync<TKey>(Expression<Func<TipoRol, bool>> predicate, Expression<Func<TipoRol, TKey>> orderBy, PaginationDto pagination)
        {
            var queryable=context.Set<TipoRol>()
                .Where(predicate)
                .OrderBy(orderBy)
                .AsNoTracking()
                .AsQueryable();

            await httpContextAccessor.HttpContext.InsertarPaginacionHeader(queryable);
            var response=await queryable.Paginate(pagination).ToListAsync();
            return response;
        }
    }
}
