using Goreu.Dto.Request;
using Goreu.Entities;
<<<<<<< Updated upstream
using System.Linq.Expressions;
=======
using Goreu.Entities.Info;
>>>>>>> Stashed changes

namespace Goreu.Repositories.Interface
{
    public interface IUserRepository
    {

        Task<Usuario?> GetAsync(string id);
<<<<<<< Updated upstream

        Task<ICollection<Usuario>> GetAsync<TKey>(Expression<Func<Usuario, bool>> predicate, Expression<Func<Usuario, TKey>> orderBy, PaginationDto pagination);
=======
        Task<ICollection<UsuarioInfo>> GetAsyncAll(string? nombres, PaginationDto pagination);
>>>>>>> Stashed changes
    }
}
