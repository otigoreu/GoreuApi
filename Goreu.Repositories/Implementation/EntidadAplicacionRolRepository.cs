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
    public class EntidadAplicacionRolRepository : RepositoryBase<EntidadAplicacionRol>, IEntidadAplicacionRolRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public EntidadAplicacionRolRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ICollection<EntidadAplicacionRol>> GetAsync<TKey>(Expression<Func<EntidadAplicacionRol, bool>> predicate, Expression<Func<EntidadAplicacionRol, TKey>> orderBy, PaginationDto pagination)
        {
            var queryable = context.Set<EntidadAplicacionRol>()

                .Where(predicate)
                .OrderBy(orderBy)
                .AsNoTracking()
                .AsQueryable();

            await httpContextAccessor.HttpContext.InsertarPaginacionHeader(queryable);
            var response = await queryable.Paginate(pagination).ToListAsync();

            return response;
        }
    }
}
