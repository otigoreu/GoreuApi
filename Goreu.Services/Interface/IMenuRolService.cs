namespace Goreu.Services.Interface
{
    public interface IMenuRolService
    {
        Task<BaseResponseGeneric<int>> AddAsync(MenuRolRequestDto request);
        Task<BaseResponse> DeleteAsync(int id);
        Task<BaseResponse> UpdateAsync(int id, MenuRolRequestDto request);
        Task<BaseResponseGeneric<MenuRolResponseDto>> GetAsync(int id);
        Task<BaseResponseGeneric<ICollection<MenuRolResponseDto>>> GetAsync();
        Task<BaseResponse> FinalizedAsync(int id);
        Task<BaseResponse> InitializedAsync(int id);
        Task<BaseResponseGeneric<ICollection<MenuRolInfo>>> GetMenusConEstadoPorRolAsync(int idEntidad,int idAplicacion,string IdMenuRol);
        Task<BaseResponseGeneric<MenuRolInfo>> GetAsync(string idRol, int idMenu);
    }
}
