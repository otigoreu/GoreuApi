namespace Goreu.Repositories.Implementation
{
    public class EntidadAplicacionRepository : RepositoryBase<EntidadAplicacion>, IEntidadAplicacionRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public EntidadAplicacionRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ICollection<Aplicacion>> GetAplicacionesAsync<TKey>(
            Expression<Func<EntidadAplicacion, bool>> predicate,
            Expression<Func<EntidadAplicacion, TKey>> orderBy,
            PaginationDto? pagination)
        {
            var queryable = context.Set<EntidadAplicacion>()
                .Where(predicate)
                .OrderBy(orderBy)
                .Select(ea => ea.Aplicacion)
                .AsNoTracking();

            if (pagination is not null)
            {
                await httpContextAccessor.HttpContext.InsertarPaginacionHeader(queryable);
                queryable = queryable.Paginate(pagination);
            }

            return await queryable.ToListAsync().ConfigureAwait(false);
        }


        public async Task<ICollection<EntidadAplicacion>> GetAsync<TKey>(Expression<Func<EntidadAplicacion, bool>> predicate, Expression<Func<EntidadAplicacion, TKey>> orderBy, PaginationDto pagination)
        {
            var queryable = context.Set<EntidadAplicacion>()
                
                .Where(predicate)
                .OrderBy(orderBy)
                .AsNoTracking()
                .AsQueryable();

            await httpContextAccessor.HttpContext.InsertarPaginacionHeader(queryable);
            var response = await queryable.Paginate(pagination).ToListAsync();

            return response;
        }

        public async Task<EntidadAplicacion> GetAsync(int idEntidad, int idAplicacion)
        {
            return context.Set<EntidadAplicacion>()
               .FirstOrDefault(z => z.IdEntidad == idEntidad && z.IdAplicacion == idAplicacion);
        }
    }
}
