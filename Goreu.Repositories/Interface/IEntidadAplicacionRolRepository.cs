using Goreu.Dto.Request;
using Goreu.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Repositories.Interface
{
    public interface IEntidadAplicacionRolRepository : IRepositoryBase<EntidadAplicacionRol>
    {
        Task<ICollection<EntidadAplicacionRol>> GetAsync<TKey>(Expression<Func<EntidadAplicacionRol, bool>> predicate, Expression<Func<EntidadAplicacionRol, TKey>> orderBy, PaginationDto pagination);
    }
}
