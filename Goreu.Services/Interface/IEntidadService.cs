using Goreu.Dto;
using Goreu.Dto.Request;
using Goreu.Dto.Response;

namespace Goreu.Services.Interface
{
    public interface IEntidadService : IServiceBase<EntidadRequestDto, EntidadResponseDto>
    {
        Task<BaseResponseGeneric<ICollection<EntidadResponseDto>>> GetAsync(string userId,  string? descripcion = null, PaginationDto? pagination = null);

        Task<BaseResponseGeneric<EntidadResponseDto>> GetAsyncPerUser(string idUser);
    }
}
