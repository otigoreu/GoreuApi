using Goreu.Services.Common;

namespace Goreu.Services.Interface
{
    public interface IUnidadOrganicaService : IServiceBase<UnidadOrganicaRequestDto, UnidadOrganicaResponseDto>
    {
        Task<BaseResponseGeneric<ICollection<UnidadOrganicaResponseDto>>> GetAsync(string descripcion, PaginationDto pagination);
        Task<BaseResponseGeneric<ICollection<UnidadOrganicaResponseDto>>> GetAsync(int idEntidad, string descripcion, PaginationDto pagination);
        Task<BaseResponseGeneric<ICollection<UnidadOrganicaResponseSingleDto>>> GetAsyncPerUser(string idUser);
        Task<BaseResponseGeneric<ICollection<UnidadOrganicaResponseDto>>> GetDescendientesJerarquicoAsync(int idUnidadOrganica);
        Task<BaseResponseGeneric<bool>> ValidarDescripcionAsync(string descripcion, int idEntidad);
        Task<BaseResponseGeneric<List<UnidadOrganicaTreeResponseDto>>> SearchTreeAsync(int idEntidad, string descripcion);
        Task<UnidadOrganicaDescendantsCountResponseDto> CountDescendantsAsync(int idEntidad, int id);

        Task<BaseResponseGeneric<List<UnidadOrganicaTreeResponseDto>>> GetTreeByUnidadOrganicaAsync(int idEntidad, int idUnidadOrganica, TipoBusquedaArbol tipoBusqueda, string descripcion);
    }
}

