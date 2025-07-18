using Goreu.Entities;
using Goreu.Persistence;
using Goreu.Repositories.Interface;
using Microsoft.AspNetCore.Http;

namespace Goreu.Repositories.Implementation
{
    public class HistorialRepository : RepositoryBase<Historial>, IHistorialRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public HistorialRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
    }
}
