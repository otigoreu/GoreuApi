using Goreu.Dto.Request;
using Goreu.Entities;
using Goreu.Entities.Info;
using System.Linq.Expressions;

namespace Goreu.Repositories.Interface
{
    public interface IEntidadRepository : IRepositoryBase<Entidad>
    {
        Task<ICollection<Entidad>> GetAsync<TKey>(Expression<Func<Entidad, bool>> predicate, Expression<Func<Entidad, TKey>> orderBy, PaginationDto? pagination = null);

        Task <EntidadInfo> GetAsyncPerUser(string iUser);
    }
}
