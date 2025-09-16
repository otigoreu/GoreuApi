using System.Runtime.CompilerServices;
using System.Text;

namespace Goreu.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContext;

        public UserRepository(ApplicationDbContext context, IHttpContextAccessor httpContext)
        {
            this.context = context;
            this.httpContext = httpContext;
        }

        public async Task<Usuario?> GetAsync(string id)
        {
            //return await context.Set<Usuario>().Include(x => x.UsuarioAplicaciones).Where(x => x.Id == id).FirstOrDefaultAsync();
            throw new NotImplementedException();
        }

        public Task<Usuario?> GetByPersonaAsync(int idPersona) => context.Set<Usuario>().FirstOrDefaultAsync(x => x.IdPersona == idPersona);

        public async Task<ICollection<UsuarioInfo>> GetAsyncAll(string? userName, PaginationDto pagination)
        {
            var queryable = context.Set<Usuario>()
                 .Include(x => x.Persona)

                 .Where(x => x.UserName.Contains(userName ?? string.Empty))
                 .AsNoTracking()
                 .Select(x => new UsuarioInfo
                 {
                     Id = x.Id,
                     UserName = x.UserName,
                     Email = x.Email,
                     IdPersona = x.Persona.Id,
                     Nombres = x.Persona.Nombres,
                     ApellidoPat = x.Persona.ApellidoPat,
                     ApellidoMat = x.Persona.ApellidoMat


                 }).AsQueryable();

            await httpContext.HttpContext.InsertarPaginacionHeader(queryable);
            return await queryable.OrderBy(x => x.Id).Paginate(pagination).ToArrayAsync();

        }

        public async Task<ICollection<Usuario>> GetAsync<TKey>(Expression<Func<Usuario, bool>> predicate, Expression<Func<Usuario, TKey>> orderBy, PaginationDto pagination)
        {
            var queryable = context.Users
                .Include(z => z.Persona)
                .Include(z => z.UsuarioUnidadOrganicas.Where(ea => ea.Estado))

                .Where(predicate)
                .OrderBy(orderBy)
                .AsNoTracking();

            await httpContext.HttpContext.InsertarPaginacionHeader(queryable);

            return await queryable.Paginate(pagination).ToListAsync();
        }

        //public async Task<ICollection<UsuarioInfo>> GetByRolAsync(int idAplicacion, string search, PaginationDto pagination)
        //{
        //    search = string.IsNullOrWhiteSpace(search) ? "" : search;

        //    var sql = GetUsuarioInfoQuery("WHERE e.idAplicacion = {1}");

        //    return await ExecuteUsuarioInfoQueryAsync(
        //        FormattableStringFactory.Create(sql, search, idAplicacion), pagination);
        //}

        public async Task<ICollection<UsuarioInfo>> GetByRolAsync(int idAplicacion, string? rolId, string search, PaginationDto pagination)
        {
            search = string.IsNullOrWhiteSpace(search) ? "" : search;

            // Arrancamos con el WHERE base
            var where = new StringBuilder("WHERE e.idAplicacion = {1}");
            var parameters = new List<object?> { search, idAplicacion };

            // Si existe rol, lo agregamos
            if (!string.IsNullOrEmpty(rolId))
            {
                where.Append(" AND d.Id = {2}");
                parameters.Add(rolId);
            }

            // Ahora construimos el SQL completo pasando el WHERE dinámico
            var sql = GetUsuarioInfoQuery(where.ToString());

            return await ExecuteUsuarioInfoQueryAsync(
                FormattableStringFactory.Create(sql, parameters.ToArray()), pagination);
        }

        private async Task<ICollection<UsuarioInfo>> ExecuteUsuarioInfoQueryAsync(
            FormattableString sql, PaginationDto pagination)
        {
            var queryable = context.UsuariosInfo.FromSqlInterpolated(sql);

            // acá ordenas con LINQ
            // ✅ Usa OrderBy encadenado en vez de new { ... }
            queryable = queryable
                .OrderBy(u => u.ApellidoPat)
                .ThenBy(u => u.ApellidoMat)
                .ThenBy(u => u.Nombres);

            await httpContext.HttpContext.InsertarPaginacionHeader(queryable);

            return await queryable.Paginate(pagination).ToListAsync();
        }

        private string GetUsuarioInfoQuery(string extraWhere = "")
        {
            return $@"
                SELECT	
                    b.Id,
                    d.Name AS Rol_Descripcion,
                    b.IdPersona,
                    b.Email,
                    b.UserName,
                    c.Nombres,
                    c.ApellidoPat,
                    c.ApellidoMat,
                    f.Descripcion AS Entidad_Descripcion,
                    g.Descripcion AS Aplicacion_Descripcion,
                    COUNT(h.IdUnidadOrganica) AS CantidadUnidadOrganica
                FROM [Administrador].[UsuarioRol] a
                INNER JOIN [Administrador].[Usuario] b ON a.UserId = b.Id
                INNER JOIN [Administrador].[Persona] c ON b.IdPersona = c.Id
                    AND (
                        c.Nombres LIKE '%' + {{0}} + '%' 
                        OR c.ApellidoPat LIKE '%' + {{0}} + '%' 
                        OR c.ApellidoMat LIKE '%' + {{0}} + '%'
                    )
                INNER JOIN [Administrador].[Rol] d ON a.RoleId = d.Id
                INNER JOIN [Administrador].[EntidadAplicacion] e ON d.IdEntidadAplicacion = e.Id
                INNER JOIN [Administrador].[Entidad] f ON e.IdEntidad = f.Id
                INNER JOIN [Administrador].[Aplicacion] g ON e.IdAplicacion = g.Id
                LEFT JOIN [Administrador].[UsuarioUnidadOrganica] h ON a.UserId = h.IdUsuario
                {extraWhere}
                GROUP BY 
                    b.Id, d.Name, b.IdPersona, b.Email, b.UserName, 
                    c.Nombres, c.ApellidoPat, c.ApellidoMat, 
                    f.Descripcion, g.Descripcion";
        }

        public async Task FinalizeAsync(string id)
        {
            var item = await context.Set<Usuario>()
                .FirstOrDefaultAsync(x => x.Id == id); // sin AsNoTracking

            if (item is null)
                throw new InvalidOperationException($"No se encontró el registro con id {id}");

            item.Estado = false;
            await context.SaveChangesAsync();
        }

        public async Task InitializeAsync(string id)
        {
            var item = await context.Set<Usuario>()
                .FirstOrDefaultAsync(x => x.Id == id); // sin AsNoTracking

            if (item is null)
                throw new InvalidOperationException($"No se encontró el registro con id {id}");

            item.Estado = true;
            await context.SaveChangesAsync();
        }
    }
}
