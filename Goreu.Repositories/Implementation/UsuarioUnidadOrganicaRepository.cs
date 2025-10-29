using Goreu.Dto.Request;
using Goreu.Dto.Response;
using Goreu.Entities;
using Goreu.Persistence;
using Goreu.Repositories.Interface;
using Goreu.Repositories.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Goreu.Repositories.Implementation
{
    public class UsuarioUnidadOrganicaRepository : RepositoryBase<UsuarioUnidadOrganica>, IUsuarioUnidadOrganicaRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UsuarioUnidadOrganicaRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task FinalizeAsync(int id, string observacionAnulacion)
        {
            var item = await context.Set<UsuarioUnidadOrganica>()
                .FirstOrDefaultAsync(x => x.Id == id); // sin AsNoTracking

            if (item is null)
                throw new InvalidOperationException($"No se encontró el registro con id {id}");

            item.Estado = false;
            item.ObservacionAnulacion = observacionAnulacion;
            item.FechaAnulacion = DateTime.Now;

            await context.SaveChangesAsync();
        }

        public async Task<ICollection<UsuarioUnidadOrganica>> GetAsync<TKey>(
            Expression<Func<UsuarioUnidadOrganica, bool>> predicate,
            Expression<Func<UsuarioUnidadOrganica, TKey>> orderBy,
            PaginationDto? pagination = null)
        {
            var queryable = context.Set<UsuarioUnidadOrganica>()
                .Include(x => x.Usuario)
                .Include(x => x.UnidadOrganica)
                .Where(predicate)
                .OrderBy(orderBy)
                .AsNoTracking()
                .AsQueryable();

            // Si hay paginación, insertar cabecera y aplicar paginación
            if (pagination is not null)
            {
                await httpContextAccessor.HttpContext.InsertarPaginacionHeader(queryable);
                queryable = queryable.Paginate(pagination);
            }

            var response = await queryable.ToListAsync();
            return response;
        }


        public async Task<UsuarioUnidadOrganica> GetAsync(int idUnidadOrganica, string idUsuario)
        {
            return context.Set<UsuarioUnidadOrganica>()
               .FirstOrDefault(z => z.IdUnidadOrganica == idUnidadOrganica && z.IdUsuario == idUsuario);
        }

        public async Task<ICollection<Usuario>> GetUsuariosAsignadosAsync(
            int idEntidad,
            int idAplicacion,
            int idUnidadOrganica)
        {
            var usuarios = await context.Set<Usuario>()
                .Include(u => u.Persona) // ✅ EF entiende esta navegación
                .AsNoTracking()
                .Where(u =>
                    u.UsuarioRoles.Any(ur =>
                        ur.Rol.EntidadAplicacion.IdEntidad == idEntidad &&
                        ur.Rol.EntidadAplicacion.IdAplicacion == idAplicacion) &&
                    u.UsuarioUnidadOrganicas.Any(uuo =>
                        uuo.UnidadOrganica.IdEntidad == idEntidad &&
                        uuo.UnidadOrganica.Id == idUnidadOrganica))
                .ToListAsync();

            return usuarios;
        }


        //public async Task<ICollection<Usuario>> GetUsuariosAsignadosAsync(int idEntidad, int idAplicacion, int idUnidadOrganica)
        //{
        //    var query = from a in context.Set<UsuarioRol>()
        //             .Where(z => z.Rol.EntidadAplicacion.IdEntidad == idEntidad
        //                      && z.Rol.EntidadAplicacion.IdAplicacion == idAplicacion)
        //                join b in context.Set<UsuarioUnidadOrganica>()
        //                    .Where(z => z.UnidadOrganica.IdEntidad == idEntidad
        //                             && z.UnidadOrganica.Id == idUnidadOrganica)
        //                    on a.UserId equals b.IdUsuario
        //                select b.Usuario;

        //    var usuarios = await query
        //        .Include(u => u.Persona) // 👈 ahora sí aplica
        //        .AsNoTracking()
        //        .ToListAsync();

        //    return await query.AsNoTracking().ToListAsync();
        //}

    }
}
