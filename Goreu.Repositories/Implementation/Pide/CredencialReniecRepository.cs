using Goreu.Entities.Info;
using Goreu.Entities.Pide;
using Goreu.Persistence;
using Goreu.Repositories.Interface.Pide;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Goreu.Repositories.Implementation.Pide
{
    public class CredencialReniecRepository : RepositoryBase<CredencialReniec>, ICredencialReniecRepository
    {
        private readonly IHttpContextAccessor httpContext;

        public CredencialReniecRepository(ApplicationDbContext context, IHttpContextAccessor httpContext) : base(context)
        {
            this.httpContext = httpContext;
        }

        public async Task<CredencialReniecInfo> GetByNumdocAsync(string nuDniUsuario)
        {
            var credencialReniec = await context.Set<CredencialReniec>()
                .Where(x => x.Persona.NroDoc == nuDniUsuario)
                .AsNoTracking()
                .Select(x => new CredencialReniecInfo
                {
                    nuRucUsuario = x.nuRucUsuario,
                    password = x.password
                }).FirstOrDefaultAsync();

            if (credencialReniec == null) return null;

            return credencialReniec;
        }

        Task ICredencialReniecRepository.FinalizedAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<ICollection<CredencialReniecInfo>> ICredencialReniecRepository.GetAsync(string? descripcion)
        {
            throw new NotImplementedException();
        }

        Task ICredencialReniecRepository.InitializedAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
