using Goreu.Dto.Request;
using Goreu.Entities;
<<<<<<< Updated upstream
=======
using Goreu.Entities.Info;
>>>>>>> Stashed changes
using Goreu.Persistence;
using Goreu.Repositories.Interface;
using Goreu.Repositories.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
<<<<<<< Updated upstream
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
=======
>>>>>>> Stashed changes

namespace Goreu.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
<<<<<<< Updated upstream
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
=======
        private readonly IHttpContextAccessor httpContext;

        public UserRepository(ApplicationDbContext context, IHttpContextAccessor httpContext)
        {
            this.context = context;
            this.httpContext = httpContext;
>>>>>>> Stashed changes
        }
        public async Task<Usuario?> GetAsync(string id)
        {
            //return await context.Set<Usuario>().Include(x => x.UsuarioAplicaciones).Where(x => x.Id == id).FirstOrDefaultAsync();
            throw new NotImplementedException();
        }

<<<<<<< Updated upstream
        public async Task<ICollection<Usuario>> GetAsync<TKey>(Expression<Func<Usuario, bool>> predicate, Expression<Func<Usuario, TKey>> orderBy, PaginationDto pagination)
        {
            var queryable = context.Users
                .Include(z=> z.Persona)

                .Where(predicate)
                .OrderBy(orderBy)
                .AsNoTracking();

            await httpContextAccessor.HttpContext.InsertarPaginacionHeader(queryable);

            return await queryable.Paginate(pagination).ToListAsync();
=======
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

>>>>>>> Stashed changes
        }
    }
}
