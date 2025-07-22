using Goreu.Dto.Request;
using Goreu.Entities;
using System.Linq.Expressions;

namespace Goreu.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<Usuario?> GetAsync(string id);

        Task<ICollection<Usuario>> GetAsync<TKey>(Expression<Func<Usuario, bool>> predicate, Expression<Func<Usuario, TKey>> orderBy, PaginationDto pagination);
    }
}
