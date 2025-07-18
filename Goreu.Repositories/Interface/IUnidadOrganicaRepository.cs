using Goreu.Dto.Request;
using Goreu.Entities;
using Goreu.Entities.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Repositories.Interface
{
    public interface IUnidadOrganicaRepository : IRepositoryBase<UnidadOrganica>
    {
        Task<ICollection<UnidadOrganica>> GetAsync<TKey>(Expression<Func<UnidadOrganica, bool>> predicate, Expression<Func<UnidadOrganica, TKey>> orderBy, PaginationDto pagination);

        Task<ICollection<UnidadOrganicaInfo>> GetAsyncPerUser(string idUser);
    }
}
