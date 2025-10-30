namespace Goreu.Repositories.Interface
{
    public interface IUnidadOrganicaRepository : IRepositoryBase<UnidadOrganica>
    {
        Task<ICollection<UnidadOrganica>> GetAsync<TKey>(Expression<Func<UnidadOrganica, bool>> predicate, Expression<Func<UnidadOrganica, TKey>> orderBy, PaginationDto pagination);
        Task<ICollection<UnidadOrganicaInfo>> GetAsyncPerUser(string idUser);
        Task<ICollection<UnidadOrganica>> ObtenerDescendientesAsync(int idUnidadOrganica);
    }
}
