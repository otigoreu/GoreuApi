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
    public interface IEntidadAplicacionRepository : IRepositoryBase<EntidadAplicacion>
    {
        Task<ICollection<Aplicacion>> GetAplicacionesAsync<TKey>(
            Expression<Func<EntidadAplicacion, bool>> predicate,
            Expression<Func<EntidadAplicacion, TKey>> orderBy,
            PaginationDto? pagination);
        Task<ICollection<EntidadAplicacion>> GetAsync<TKey>(
            Expression<Func<EntidadAplicacion, bool>> predicate, 
            Expression<Func<EntidadAplicacion, TKey>> orderBy, 
            PaginationDto pagination);
        Task<EntidadAplicacion> GetAsync(
            int idEntidad, 
            int idAplicacion);
    }
}
