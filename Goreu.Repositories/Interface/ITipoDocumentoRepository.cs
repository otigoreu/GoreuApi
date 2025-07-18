using Goreu.Entities;
using Goreu.Entities.Info;

namespace Goreu.Repositories.Interface
{
    public interface ITipoDocumentoRepository : IRepositoryBase<TipoDocumento>
    {
        Task<ICollection<TipoDocumentoInfo>> GetAsync(string? descripcion);
        Task FinalizedAsync(int id);
        Task InitializedAsync(int id);
    }
}
