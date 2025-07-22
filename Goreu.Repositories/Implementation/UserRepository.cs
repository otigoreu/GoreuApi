using Goreu.Dto.Request;
using Goreu.Entities;
using Goreu.Entities.Info;
using Goreu.Persistence;
using Goreu.Repositories.Interface;
using Goreu.Repositories.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

        public async Task<ICollection<Usuario>> GetAsync<TKey>(Expression<Func<Usuario, bool>> predicate, Expression<Func<Usuario, TKey>> orderBy, PaginationDto pagination)
        {
            var queryable = context.Users
        .Include(z => z.Persona)
        .Where(predicate)
        .OrderBy(orderBy)
        .AsNoTracking();

            await httpContext.HttpContext.InsertarPaginacionHeader(queryable);

            return await queryable.Paginate(pagination).ToListAsync();
        }

        public async Task<ICollection<UsuarioInfo>> GetAsyncAll(string? userName, PaginationDto pagination)
        {
           var queryable =context.Set<Usuario>()
                .Include(x=>x.Persona)
                .Where(x=>x.UserName.Contains(userName??string.Empty))
                .AsNoTracking()
                .Select(x=>new UsuarioInfo 
                { 
                    Id=x.Id,
                    UserName=x.UserName,
                    Email=x.Email,
                    idPersona=x.Persona.Id,
                    Nombres=x.Persona.Nombres,
                    ApellidoPat=x.Persona.ApellidoPat,
                    ApellidoMat=x.Persona.ApellidoMat


                }).AsQueryable();

            await httpContext.HttpContext.InsertarPaginacionHeader(queryable);
            return await queryable.OrderBy(x=>x.Id).Paginate(pagination).ToArrayAsync();

        }

        
    }
}
