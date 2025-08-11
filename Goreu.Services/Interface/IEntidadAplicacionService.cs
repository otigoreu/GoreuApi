using Goreu.Dto.Request;
using Goreu.Dto.Response;

namespace Goreu.Services.Interface
{
    public interface IEntidadAplicacionService : IServiceBase<EntidadAplicacionRequestDto, EntidadAplicacionResponseDto>
    {
        Task<BaseResponseGeneric<ICollection<EntidadAplicacionResponseDto>>> GetAplicacionesConEstadoPorEntidadAsync(int idEntidad, string descripcion, PaginationDto pagination);
        Task<BaseResponseGeneric<ICollection<AplicacionResponseDto>>> GetAplicacionesAsync(int idEntidad);
        Task<BaseResponseGeneric<EntidadAplicacionResponseDto>> GetAsync(int idEntidad, int idAplicacion);
    }
}
