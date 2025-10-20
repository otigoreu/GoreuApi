namespace Goreu.Repositories.Interface
{
    public interface IUserRoleRepository
    {
        Task<ICollection<UsuarioRol>> GetUsuarioAsync(int idEntidad, int idAplicacion, string search, PaginationDto pagination);
        Task FinalizeAsync(Guid id);
        Task InitializeAsync(Guid id);
    }
}
