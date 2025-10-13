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
        Task<Usuario?> GetAsync(Expression<Func<Usuario, bool>> predicate);
        Task<Usuario?> GetByPersonaAsync(int idPersona);
        Task<ICollection<UsuarioInfo>> GetAsyncAll(string? nombres, PaginationDto pagination);
        Task<ICollection<Usuario>> GetAsync<TKey>(Expression<Func<Usuario, bool>> predicate, Expression<Func<Usuario, TKey>> orderBy, PaginationDto pagination);
        Task<ICollection<UsuarioInfo>> GetByRolAsync(int idAplicacion, string? rolId, string? search, PaginationDto? pagination);
        //Task<ICollection<UsuarioInfo>> GetByEntidadAsync(int idEntidad, string search, PaginationDto pagination);
        //Task<ICollection<UsuarioInfo>> GetAllAsync(string search, PaginationDto pagination);
        Task FinalizeAsync(string id);
        Task InitializeAsync(string id);
        Task MarkPasswordAsMustChangeAsync(string userId);
    }
}
