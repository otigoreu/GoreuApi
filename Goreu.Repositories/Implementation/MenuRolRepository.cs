


namespace Goreu.Repositories.Implementation
{
    public class MenuRolRepository : RepositoryBase<MenuRol>, IMenuRolRepository
    {
        IHttpContextAccessor httpContext;
        public MenuRolRepository(ApplicationDbContext context, IHttpContextAccessor httpContext) : base(context)
        {
            this.httpContext = httpContext;
        }

        public async Task FinalizedAsync(int id)
        {
            var menuRol=await GetAsync(id);
            if (menuRol is not null) {
                menuRol.Estado = false;
                await UpdateAsync();
            }
        }

        public Task<MenuRol> GetAsync(string idRol, int idMenu)
        {
            return context.Set<MenuRol>()
                .AsNoTracking().FirstOrDefaultAsync(z=> z.IdRol==idRol && z.IdMenu==idMenu);
        }

        public async Task InitializedAsync(int id)
        {
            var menuRol = await GetAsync(id);
            if (menuRol is not null)
            {
                menuRol.Estado = true;
                await UpdateAsync();
            }
        }
    }
}
