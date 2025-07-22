using Goreu.Dto.Request;
using Goreu.Entities;
using System.Linq.Expressions;

namespace Goreu.Repositories.Interface
{
    public interface IUsuarioUnidadOrganicaRepository : IRepositoryBase<UsuarioUnidadOrganica>
    {
        Task<ICollection<UsuarioUnidadOrganica>> GetAsync<TKey>(Expression<Func<UsuarioUnidadOrganica, bool>> predicate, Expression<Func<UsuarioUnidadOrganica, TKey>> orderBy, PaginationDto pagination);

        Task<UsuarioUnidadOrganica> GetAsync(int idUnidadOrganica, string idUsuario);
    }
}
