using Goreu.Entities;
using Goreu.Entities.Info;

namespace Goreu.Repositories.Interface
{
    public interface IMenuRepository : IRepositoryBase<Menu>
    {
        Task<ICollection<Menu>> GetByIdAplicationAsync(int idAplication);
        Task<List<Menu>> GetMenusByApplicationAndRolesAsync(int applicationId, List<string> roleIds);
        Task<ICollection<MenuInfo>> GetAsync(string? Descripcion);
        Task<ICollection<MenuInfoRol>> GetAsyncWithRole(string? Descripcion);
        Task FinalizedAsync(int id);
        Task InitializedAsync(int id);
    }
}
