using Goreu.Dto.Request;
using Goreu.Entities;
using Goreu.Persistence;
using Goreu.Repositories.Interface;
using Goreu.Repositories.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<Usuario?> GetAsync(string id)
        {
            //return await context.Set<Usuario>().Include(x => x.UsuarioAplicaciones).Where(x => x.Id == id).FirstOrDefaultAsync();
            throw new NotImplementedException();
        }

        public async Task<ICollection<Usuario>> GetAsync<TKey>(Expression<Func<Usuario, bool>> predicate, Expression<Func<Usuario, TKey>> orderBy, PaginationDto pagination)
        {
            var queryable = context.Users
                .Include(z=> z.Persona)

                .Where(predicate)
                .OrderBy(orderBy)
                .AsNoTracking();

            await httpContextAccessor.HttpContext.InsertarPaginacionHeader(queryable);

            return await queryable.Paginate(pagination).ToListAsync();
        }
    }
}
