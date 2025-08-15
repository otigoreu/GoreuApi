using Goreu.Dto.Request;
using Goreu.Entities;
using System.Linq.Expressions;

namespace Goreu.Repositories.Interface
{
    public interface ITipoRolRepository : IRepositoryBase<TipoRol>
    {
        Task<ICollection<TipoRol>> GetAsync<TKey>(Expression<Func<TipoRol, bool>> predicate, Expression<Func<TipoRol, TKey>> orderBy, PaginationDto pagination);

    }
}
