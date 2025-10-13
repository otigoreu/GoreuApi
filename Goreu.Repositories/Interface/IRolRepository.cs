namespace Goreu.Repositories.Interface
{
    public interface IRolRepository
    {
        Task<ICollection<Rol>> GetAllAsync();
        Task<ICollection<RolEntidadAplicacionInfo>> GetWithAllEntidadAplicacionsync();
        Task<ICollection<RolInfo>> GetAsyncPerUser(string idUser);
        Task<Rol?> GetAsync(string id);
        Task<string> AddAsync(Rol rol);
        Task DeleteAsync(string id);
        Task FinalizedAsync(string id);
        Task InitializedAsync(string id);
        Task<ICollection<Rol>> GetAsync(
            int idEntidad,
            int idAplicacion,
            string? search,
            PaginationDto? pagination,
            string? rolId = null);
    }
}
