using Goreu.Entities;
using Goreu.Persistence;
using Goreu.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Usuario?> GetAsync(string id)
        {
            //return await context.Set<Usuario>().Include(x => x.UsuarioAplicaciones).Where(x => x.Id == id).FirstOrDefaultAsync();
            throw new NotImplementedException();
        }
    }
}
