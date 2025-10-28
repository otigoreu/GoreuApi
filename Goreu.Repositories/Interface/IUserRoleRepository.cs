namespace Goreu.Repositories.Interface
{
    public interface IUserRoleRepository
    {
        //Task<ICollection<UsuarioRol>> GetUsuarioAsync(int idEntidad, int idAplicacion, string search, PaginationDto pagination);
        Task<ICollection<Usuario>> GetUsuarioAsync(int idEntidad, int idAplicacion, string? rolId, string search, PaginationDto pagination);
        Task<Guid> AddAsync(UsuarioRol entity);
        Task<UsuarioRol?> GetAsync(string userId, string rolId);
        Task FinalizeAsync(Guid id);
        Task InitializeAsync(Guid id);
    }
}
