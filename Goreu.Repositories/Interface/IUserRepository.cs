using Goreu.Dto.Request;
using Goreu.Dto.Response;
using Goreu.Entities;
using Goreu.Entities.Info;
using System.Linq.Expressions;

namespace Goreu.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<Usuario?> GetAsync(string id);
        Task<ICollection<UsuarioInfo>> GetAsyncAll(string? nombres, PaginationDto pagination);
        Task<ICollection<Usuario>> GetAsync<TKey>(Expression<Func<Usuario, bool>> predicate, Expression<Func<Usuario, TKey>> orderBy, PaginationDto pagination);
        Task<ICollection<UsuarioInfo>> GetByRolAsync(int idAplicacion, string search, PaginationDto pagination);
        Task<ICollection<UsuarioInfo>> GetByEntidadAsync(int idEntidad, string search, PaginationDto pagination);
        Task<ICollection<UsuarioInfo>> GetAllAsync(string search, PaginationDto pagination);
        Task FinalizeAsync(string id);
        Task InitializeAsync(string id);
       
    }
}
