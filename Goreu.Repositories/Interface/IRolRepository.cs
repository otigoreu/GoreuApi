using Goreu.Entities;

namespace Goreu.Repositories.Interface
{
    public interface IRolRepository
    {
        Task<ICollection<Rol>> GetAllAsync();
        Task<Rol?> GetAsync(string id);
        Task<string> AddAsync(Rol rol);
        Task DeleteAsync(string id);
        Task FinalizedAsync(string id);
        Task InitializedAsync(string id);
    }
}
