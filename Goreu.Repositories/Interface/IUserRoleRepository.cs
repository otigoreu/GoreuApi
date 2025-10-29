using Goreu.Dto.Response;

namespace Goreu.Repositories.Interface
{
    public interface IUserRoleRepository
    {
        //Task<ICollection<UsuarioRol>> GetUsuarioAsync(int idEntidad, int idAplicacion, string search, PaginationDto pagination);
        Task<ICollection<Usuario>> GetUsuarioAsync(int idEntidad, int idAplicacion, string? rolId, string search, PaginationDto pagination);
        Task<ICollection<RolConAsignacionDto>> GetRolesConAsignacionAsync(int idEntidad, int idAplicacion, string userId);
        Task AsignarRoleAsync(int idEntidad, int idAplicacion, string userId, string rolId, bool selected);
        Task<Guid> AddAsync(UsuarioRol entity);
        Task<UsuarioRol?> GetAsync(string userId, string rolId);
        Task FinalizeAsync(Guid id);
        Task InitializeAsync(Guid id);
    }
}
