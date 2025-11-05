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
                @"select r.Id, r.Name from adm.Rol r 
                join adm.UsuarioRol ur on r.Id=ur.RoleId 
                join adm.Usuario u on u.Id=ur.UserId where u.Id={0}", idUser);

            return await query.ToListAsync();
        }

        public async Task<ICollection<Rol>> GetAsync(
            int idEntidad,
            int idAplicacion,
            string? search,
            PaginationDto? pagination,
            string? rolId)
        {
            var queryable = context.Set<Rol>()
                .Include(z => z.EntidadAplicacion.Entidad)
                .Include(z => z.EntidadAplicacion.Aplicacion)
                .Where(z => z.EntidadAplicacion.IdEntidad == idEntidad
                         && z.EntidadAplicacion.IdAplicacion == idAplicacion)
                .AsNoTracking();


            var lis = queryable.ToList();

            // Filtro de búsqueda
            if (!string.IsNullOrWhiteSpace(search))
            {
                var normalizedSearch = search.Trim().ToLower();
                queryable = queryable.Where(z => z.Name.ToLower().Contains(normalizedSearch));
            }

            // Ordenar alfabéticamente por nombre
            queryable = queryable.OrderBy(z => z.Name);

            // Paginación
            if (pagination is not null)
            {
                await httpContext.HttpContext.InsertarPaginacionHeader(queryable);
                queryable = queryable.Paginate(pagination);
            }

            return await queryable.ToListAsync().ConfigureAwait(false);
        }

        public async Task<ICollection<RolEntidadAplicacionInfo>> GetWithAllEntidadAplicacionAsync(int idEntidad, int idAplicacion)
        {
            var query = context.Set<RolEntidadAplicacionInfo>().FromSqlRaw(
                 @"select r.Id, r.Name, r.NormalizedName, r.Estado, r.IdEntidadAplicacion 
                    from adm.Rol r 
	                    join adm.EntidadAplicacion ea on ea.Id=r.IdEntidadAplicacion
	                    join adm.Aplicacion a on a.Id=ea.IdAplicacion
	                    join adm.Entidad e on e.Id=ea.IdEntidad where e.Id={0} and a.Id={1}",idEntidad,idAplicacion);

            return await query.ToListAsync();
        }

        public async Task<ICollection<RolEntidadAplicacionCounterInfo>> GetWithAllEntidadAplicacionCounterAsync(int idEntidad, int idAplicacion)
        {
            var query = context.Set<RolEntidadAplicacionCounterInfo>().FromSqlRaw(
                @"select r.Id, r.Name, r.NormalizedName, r.Estado, r.IdEntidadAplicacion, COUNT(case when mr.Estado=1 then 1 end) as CantidadMenus 
                        from adm.Rol r 
                        left join adm.MenuRol mr on r.Id=mr.IdRol
						join adm.EntidadAplicacion ea on ea.Id=r.IdEntidadAplicacion 
						where ea.IdEntidad={0} and ea.IdAplicacion={1} 
                        group by r.Id, r.Name, r.NormalizedName, r.Estado, r.IdEntidadAplicacion", idEntidad, idAplicacion);

            return await query.ToListAsync();




        }

       
    }
}