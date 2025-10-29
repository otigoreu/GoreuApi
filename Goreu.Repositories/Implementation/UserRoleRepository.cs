using Goreu.Dto.Response;
using Goreu.Entities;

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

        public async Task<ICollection<Usuario>> GetUsuarioAsync(
            int idEntidad,
            int idAplicacion,
            string? rolId,
            string? search,
            PaginationDto? pagination)
        {
            // 1️⃣ Base query
            var queryable = context.Set<Usuario>()
                            .AsNoTracking()
                            .Include(u => u.Persona)
                            .Include(u => u.UsuarioUnidadOrganicas
                                .Where(z => z.UnidadOrganica.IdEntidad == idEntidad))
                            .Include(u => u.UsuarioRoles.Where(z=> z.Rol.EntidadAplicacion.IdEntidad == idEntidad))
                                .ThenInclude(ur => ur.Rol)
                            .Where(u =>
                                !u.UsuarioRoles.Any() || // sin rol
                                u.UsuarioRoles.Any(r =>
                                    r.Rol.EntidadAplicacion.IdEntidad == idEntidad &&
                                    r.Rol.EntidadAplicacion.IdAplicacion == idAplicacion));

            if (rolId != null)
                queryable = queryable.Where(u =>
                    u.UsuarioRoles.Any(ur => ur.RoleId == rolId));

            // 2️⃣ Filtro de búsqueda
            if (!string.IsNullOrWhiteSpace(search))
            {
                queryable = queryable.Where(x =>
                    x.UserName.Contains(search) ||
                    x.Persona.Nombres.Contains(search) ||
                    x.Persona.ApellidoPat.Contains(search) ||
                    x.Persona.ApellidoMat.Contains(search));
            }

            // 3️⃣ Inserta header de paginación (total registros)
            var contextHttp = httpContext.HttpContext;
            if (contextHttp is not null)
                await contextHttp.InsertarPaginacionHeader(queryable);

            // 4️⃣ Paginación
            if (pagination is not null)
            {
                pagination.Page = pagination.Page < 0 ? 0 : pagination.Page;
                pagination.RecordsPerPage = pagination.RecordsPerPage <= 0 ? 50 : pagination.RecordsPerPage;

                queryable = queryable
                    .OrderBy(x => x.Id)
                    .Paginate(pagination);
            }
            else
            {
                queryable = queryable.OrderBy(x => x.Id);
            }

            // 5️⃣ Devuelve la lista de usuarios
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

        public async Task<ICollection<RolConAsignacionDto>> GetRolesConAsignacionAsync(int idEntidad, int idAplicacion, string userId)
        {
            var query =
                from rol in context.Set<Rol>()
                    .Where(r => r.EntidadAplicacion.IdEntidad == idEntidad && r.EntidadAplicacion.IdAplicacion == idAplicacion)
                join usuarioRol in context.Set<UsuarioRol>()
                    .Where(ur => ur.UserId == userId)
                    on rol.Id equals usuarioRol.RoleId into asignaciones
                from asignacion in asignaciones.DefaultIfEmpty() // 👈 left join
                select new RolConAsignacionDto
                {
                    Id = rol.Id,
                    Descripcion = rol.Name,
                    Asignado = asignacion != null // 👈 true si tiene asignación
                };

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task AsignarRoleAsync(int idEntidad, int idAplicacion, string userId, string rolId, bool selected)
        {
            // 🔍 Validaciones rápidas de entrada
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("El identificador del usuario no puede estar vacío.", nameof(userId));

            if (string.IsNullOrWhiteSpace(rolId))
                throw new ArgumentException("El identificador del rol no puede estar vacío.", nameof(rolId));

            // 📦 Buscar si ya existe la relación entre el usuario y el rol
            var existing = await context.Set<UsuarioRol>()
                .Include(ur => ur.Rol)
                .Where(ur =>
                    ur.UserId == userId &&
                    ur.RoleId == rolId &&
                    ur.Rol.EntidadAplicacion.IdEntidad == idEntidad &&
                    ur.Rol.EntidadAplicacion.IdAplicacion == idAplicacion)
                .FirstOrDefaultAsync();

            if (existing is null)
            {
                // 🆕 Si no existe, crear solo si se debe asignar
                if (selected)
                {
                    await context.Set<UsuarioRol>().AddAsync(new UsuarioRol
                    {
                        UserId = userId,
                        RoleId = rolId,
                        Estado = true
                    });
                    await context.SaveChangesAsync();
                }
                // 🚫 Si no está seleccionado, no se crea nada
                return;
            }

            // 🔴 Si existe y se quiere desasignar, eliminar
            if (existing is not null && !selected)
            {
                context.Remove(existing);
                await context.SaveChangesAsync();
                return;
            }
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
