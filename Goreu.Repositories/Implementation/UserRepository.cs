using Goreu.Dto.Request;
using Goreu.Dto.Response;
using Goreu.Entities;
using Goreu.Entities.Info;
using Goreu.Persistence;
using Goreu.Repositories.Interface;
using Goreu.Repositories.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

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
                     idPersona = x.Persona.Id,
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

        public async Task<ICollection<Usuario>> GetAsync<TKey>(int idEntidad, Expression<Func<Usuario, bool>> predicate, Expression<Func<Usuario, TKey>> orderBy, PaginationDto pagination)
        {
            var queryable = context.Set<UsuarioUnidadOrganica>()
                .Include(z => z.Usuario.Persona)
                .Include(z => z.Usuario.UsuarioUnidadOrganicas.Where(ea => ea.Estado))

                .Where(z => z.UnidadOrganica.IdEntidad == idEntidad)
                .Select(z => z.Usuario) // Aquí ya no necesitas más Include
                .Where(predicate)
                .OrderBy(orderBy)
                .AsNoTracking();

            await httpContext.HttpContext.InsertarPaginacionHeader(queryable);

            return await queryable.Paginate(pagination).ToListAsync();
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
