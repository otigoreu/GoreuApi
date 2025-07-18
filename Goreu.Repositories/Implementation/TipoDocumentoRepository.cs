using Goreu.Entities;
using Goreu.Entities.Info;
using Goreu.Persistence;
using Goreu.Repositories.Interface;
using Goreu.Repositories.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Goreu.Repositories.Implementation
{
    public class TipoDocumentoRepository : RepositoryBase<TipoDocumento>, ITipoDocumentoRepository
    {
        private readonly IHttpContextAccessor httpContext;

        public TipoDocumentoRepository(ApplicationDbContext context, IHttpContextAccessor httpContext) : base(context)
        {
            this.httpContext = httpContext;
        }

        public async Task FinalizedAsync(int id)
        {
            var tipoDocu = await GetAsync(id);
            if (tipoDocu is not null)
            {
                tipoDocu.Estado = false;
                await UpdateAsync();
            }
        }

        public async Task<ICollection<TipoDocumentoInfo>> GetAsync(string? descripcion)
        {
            //eager loading optimizado
            var queryable = context.Set<TipoDocumento>()
                .Where(x => x.Descripcion.Contains(descripcion ?? string.Empty))
                .IgnoreQueryFilters()
                .AsNoTracking()
                .Select(x => new TipoDocumentoInfo
                {
                    Id = x.Id,
                    Descripcion = x.Descripcion,
                    Abrev = x.Abrev,
                    Estado = x.Estado

                }).AsQueryable();

            await httpContext.HttpContext.InsertarPaginacionHeader(queryable);
            return await queryable.ToListAsync();
        }

        public async Task InitializedAsync(int id)
        {

            var tipoDocu = await context.Set<TipoDocumento>().IgnoreQueryFilters().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (tipoDocu is not null)
            {
                tipoDocu.Estado = true;
                context.Set<TipoDocumento>().Update(tipoDocu);
                await context.SaveChangesAsync();

            }

        }
    }
}
