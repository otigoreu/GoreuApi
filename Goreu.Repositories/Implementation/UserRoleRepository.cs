namespace Goreu.Repositories.Implementation
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContext;

        public UserRoleRepository(ApplicationDbContext context, IHttpContextAccessor httpContext)
        {
            this.context = context;
            this.httpContext = httpContext;
        }

        public async Task<ICollection<UsuarioRol>> GetUsuarioAsync(int idEntidad, int idAplicacion, string search, PaginationDto pagination)
        {
            //var queryable = context.Set<UsuarioRol>()
            //    .AsNoTracking()
            //    .Include(x => x.Rol)
            //    .Include(x => x.Usuario)
            //    .Include(x => x.Usuario.Persona)
            //    .Include(x => x.Usuario.UsuarioUnidadOrganicas)
            //    .Where(x =>
            //        x.Rol.EntidadAplicacion.IdEntidad == idEntidad &&
            //        x.Rol.EntidadAplicacion.IdAplicacion == idAplicacion);

            var queryable = context.Set<UsuarioRol>()
                .AsNoTracking()
                .Include(x => x.Rol)
                .Include(x => x.Usuario)
                    .ThenInclude(u => u.Persona)
                .Include(x => x.Usuario)
                    .ThenInclude(u => u.UsuarioUnidadOrganicas.Where(z => z.UnidadOrganica.IdEntidad == idEntidad))
                .Where(x =>
                x.Rol.EntidadAplicacion.IdEntidad == idEntidad &&
                x.Rol.EntidadAplicacion.IdAplicacion == idAplicacion);
                //x.Usuario.UsuarioUnidadOrganicas.Any(z => z.UnidadOrganica.IdEntidad == idEntidad));


            // 🔍 Filtro de búsqueda solo si hay texto
            if (!string.IsNullOrWhiteSpace(search))
            {
                queryable = queryable.Where(x =>
                    x.Usuario.UserName.Contains(search) ||
                    x.Usuario.Persona.Nombres.Contains(search) ||
                    x.Usuario.Persona.ApellidoPat.Contains(search) ||
                    x.Usuario.Persona.ApellidoMat.Contains(search));
            }

            // 📄 Inserta header de total de registros
            var contextHttp = httpContext.HttpContext;
            if (contextHttp is not null)
                await contextHttp.InsertarPaginacionHeader(queryable);

            // 🧭 Aplica paginación (segura)
            if (pagination is not null)
            {
                // Evita valores negativos
                pagination.Page = pagination.Page < 0 ? 0 : pagination.Page;
                pagination.RecordsPerPage = pagination.RecordsPerPage <= 0 ? 50 : pagination.RecordsPerPage;

                queryable = queryable
                    .OrderBy(x => x.UserId)
                    .Paginate(pagination);
            }
            else
            {
                // Si no se envía paginación, devuelve todo ordenado
                queryable = queryable.OrderBy(x => x.UserId);
            }

            // 📦 Devuelve la lista final
            return await queryable.ToListAsync();
        }

        public async Task<Guid> AddAsync(UsuarioRol entity)
        {
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<UsuarioRol?> GetAsync(string userId, string rolId)
        {
            return await context
               .Set<UsuarioRol>()
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.UserId == userId && x.RoleId == rolId);
        }

        public async Task FinalizeAsync(Guid id)
        {
            var item = await context.Set<UsuarioRol>()
                .FirstOrDefaultAsync(x => x.Id == id); // sin AsNoTracking

            if (item is null)
                throw new InvalidOperationException($"No se encontró el registro con id {id}");

            item.Estado = false;
            await context.SaveChangesAsync();
        }

        public async Task InitializeAsync(Guid id)
        {
            var item = await context.Set<UsuarioRol>()
                .FirstOrDefaultAsync(x => x.Id == id); // sin AsNoTracking

            if (item is null)
                throw new InvalidOperationException($"No se encontró el registro con id {id}");

            item.Estado = true;
            await context.SaveChangesAsync();
        }
    }
}
