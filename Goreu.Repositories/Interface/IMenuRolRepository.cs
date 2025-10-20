using Goreu.Dto.Response;

namespace Goreu.Repositories.Interface
{
    public interface IMenuRolRepository : IRepositoryBase<MenuRol>
    {
        Task FinalizedAsync(int id);
        Task InitializedAsync(int id);
        Task<MenuRol> GetAsync(string idRol, int idMenu);
    }
}
