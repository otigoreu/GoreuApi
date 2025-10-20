namespace Goreu.Repositories.Interface
{
    public interface IMenuRepository : IRepositoryBase<Menu>
    {
        Task<ICollection<MenuInfo>> GetByIdAplicationAsync(int idAplication);
        Task<ICollection<Menu>> GetAllByEntidadAndAplicacion(int idEntidad, int idAplicaicon);
        Task<ICollection<Menu>> GetAllByRol(string idRol);
        Task<List<Menu>> GetMenusByApplicationAndRolesAsync(int applicationId, List<string> roleIds);
        Task<ICollection<MenuInfo>> GetAsync(string? Descripcion);
        Task<ICollection<MenuInfoRol>> GetAsyncWithRole(string? Descripcion);
        Task FinalizedAsync(int id);
        Task InitializedAsync(int id);
    }
}
