using Goreu.Entities;

namespace Goreu.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<Usuario?> GetAsync(string id);
    }
}
