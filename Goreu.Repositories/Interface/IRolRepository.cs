using Goreu.Dto.Response;
using Goreu.Entities;
using Goreu.Entities.Info;

namespace Goreu.Repositories.Interface
{
    public interface IRolRepository
    {
        Task<ICollection<Rol>> GetAllAsync();

        Task<ICollection<RolInfo>> GetAsyncPerUser(string idUser);
        Task<Rol?> GetAsync(string id);
        Task<string> AddAsync(Rol rol);
        Task DeleteAsync(string id);
        Task FinalizedAsync(string id);
        Task InitializedAsync(string id);
    }
}
