

namespace Goreu.Services.Interface
{
    public interface IRolService
    {
        Task<BaseResponseGeneric<string>> AddSync(RolRequestDto request);
        Task<BaseResponse> DeleteAsync(string id);
        Task<BaseResponse> UpdateAsync(string id, RolRequestDto request);
        Task<BaseResponseGeneric<ICollection<RolResponseDto>>> GetAsync();
        Task<BaseResponseGeneric<RolResponseDto>> GetAsync(string id);
        Task<BaseResponseGeneric<ICollection<RolResponseSingleDto>>> GetAsyncPerUser(string idUser);
        Task<BaseResponse> FinalizedAsync(string id);
        Task<BaseResponse> InitializedAsync(string id);
        Task<BaseResponseGeneric<ICollection<RolPaginationResponseDto>>> GetAsync(int idEntidad, int idAplicacion, string? search, PaginationDto? pagination, string rolId);
    }
}
