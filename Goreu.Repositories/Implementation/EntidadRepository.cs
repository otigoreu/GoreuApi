namespace Goreu.Repositories.Implementation
{
    public class EntidadRepository : RepositoryBase<Entidad>, IEntidadRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public EntidadRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ICollection<Entidad>> GetAsync<TKey>(Expression<Func<Entidad, bool>> predicate, Expression<Func<Entidad, TKey>> orderBy, PaginationDto? pagination = null)
        {
            var queryable = context.Set<Entidad>()
                .Include(z => z.EntidadAplicaciones.Where(ea => ea.Estado))
                .Where(predicate)
                .OrderBy(orderBy)
                .AsQueryable();

            if (pagination is not null)
            {
                await httpContextAccessor.HttpContext.InsertarPaginacionHeader(queryable);
                queryable = queryable.Paginate(pagination);
            }

            var response = await queryable.ToListAsync();
            return response;
        }

        public async Task<EntidadInfo> GetAsyncPerRol(string idRol)
        {
            var query = context.Set<EntidadInfo>().FromSqlRaw(
               @"select e.id, e.Descripcion,e.Ruc ,e.Estado from Administrador.Entidad e 
                join Administrador.EntidadAplicacion ea on e.Id=ea.IdEntidad 
                join Administrador.Rol r on r.IdEntidadAplicacion=ea.IdAplicacion where r.id={0}", idRol);

            return await query.SingleOrDefaultAsync();
        }

        public async Task <EntidadInfo> GetAsyncPerUser(string idUser)
        {
            var query = context.Set<EntidadInfo>().FromSqlRaw(
                @"select DISTINCT e.Id, e.Descripcion,e.Ruc,e.Estado from Administrador.UnidadOrganica uo 
                join Administrador.UsuarioUnidadOrganica uuo on uuo.IdUnidadOrganica= uo.Id join Administrador.Entidad e on e.Id=uo.IdEntidad
                join Administrador.Usuario u on u.Id=uuo.IdUsuario where u.Id={0}", idUser);

            //return await query.ElementAtAsync(0);
            return await query.SingleOrDefaultAsync();
        }
    }
}
