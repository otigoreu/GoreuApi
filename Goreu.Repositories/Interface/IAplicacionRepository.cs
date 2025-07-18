using Goreu.Dto.Request;
using Goreu.Entities;
using Goreu.Entities.Info;
using System.Linq.Expressions;

namespace Goreu.Repositories.Interface
{
    public interface IAplicacionRepository : IRepositoryBase<Aplicacion>
    {
        Task<ICollection<Aplicacion>> GetAsync<TKey>(Expression<Func<Aplicacion, bool>> predicate, Expression<Func<Aplicacion, TKey>> orderBy, PaginationDto pagination);

        Task<ICollection<AplicacionInfo>> GetAsyncPerUser(string idUser);

    }
}
