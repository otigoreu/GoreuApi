using Goreu.Dto;
using Goreu.Dto.Request;
using Goreu.Dto.Response;

namespace Goreu.Services.Interface
{
    public interface IEntidadService : IServiceBase<EntidadRequestDto, EntidadResponseDto>
    {
        Task<BaseResponseGeneric<ICollection<EntidadResponseDto>>> GetAsync(int idEntidad, string? descripcion, PaginationDto pagination);

        Task<BaseResponseGeneric<EntidadResponseDto>> GetAsyncPerUser(string idUser);
    }
}
