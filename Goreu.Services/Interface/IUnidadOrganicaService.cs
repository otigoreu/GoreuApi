using Goreu.Dto.Request;
using Goreu.Dto.Response;

namespace Goreu.Services.Interface
{
    public interface IUnidadOrganicaService : IServiceBase<UnidadOrganicaRequestDto, UnidadOrganicaResponseDto>
    {
        Task<BaseResponseGeneric<ICollection<UnidadOrganicaResponseDto>>> GetAsync(string descripcion, PaginationDto pagination);
        Task<BaseResponseGeneric<ICollection<UnidadOrganicaResponseDto>>> GetAsync(int idEntidad, string descripcion, PaginationDto pagination);
        Task<BaseResponseGeneric<ICollection<UnidadOrganicaResponseSingleDto>>> GetAsyncPerUser(string idUser);
    }
}

