using Goreu.Dto.Request;
using Goreu.Dto.Response;
using Goreu.Entities.Info;

namespace Goreu.Services.Interface
{
    public interface IMenuService
    {
        Task<BaseResponseGeneric<int>> AddAsync(MenuRequestDto request);
        Task<BaseResponseGeneric<int>> AddAsyncSingle(MenuRequestDtoSingle request);
        Task<BaseResponseGeneric<ICollection<MenuResponseDto>>> GetByAplicationAsync(int idAplication, string email);
        Task<BaseResponseGeneric<ICollection<MenuInfo>>> GetAsync(string? Descripcion);
        Task<BaseResponseGeneric<ICollection<MenuInfoRol>>> GetAsyncWithRole(string? Descripcion);
        Task<BaseResponse> DeleteAsync(int id);
        Task<BaseResponse> UpdateAsync(int id, MenuRequestDto request);
        Task<BaseResponse> UpdateAsyncSingle(int id, MenuRequestDtoSingle request);
        Task<BaseResponse> FinalizedAsync(int id);
        Task<BaseResponse> InitializedAsync(int id);
    }
}
