using Goreu.Dto.Request;
using Goreu.Dto.Response;
using Goreu.DtoResponse;
using Goreu.Entities.Info;

namespace Goreu.Services.Interface
{
    public interface IUserService
    {
        Task<BaseResponseGeneric<RegisterResponseDto>> RegisterAsync(RegisterRequestDto request);
        Task<BaseResponseGeneric<LoginResponseDto>> LoginAsync(LoginRequestDto request);
        //-----------------------------------------------------------------------------------------
        Task<BaseResponse> RequestTokenToResetPasswordAsync(ResetPasswordRequestDto request);
        Task<BaseResponse> ResetPasswordAsync(NewPasswordRequestDto request);
        Task<BaseResponse> ChangePasswordAsyncEmail(string email, ChangePasswordRequestDto request);
        Task<BaseResponse> ChangePasswordAsyncUserName(string userName, ChangePasswordRequestDto request);
        //--------------------------------------------------------------------------------------------
        Task<BaseResponseGeneric<List<UsuarioResponseDto>>> GetUsersByRole(string? role);
        Task<BaseResponseGeneric<UsuarioResponseDto>> GetUserByEmail(string email);
        //--------------------------------------------------------------------------------------------
        Task<BaseResponse> GrantUserRole(string userId, string roleName);
        Task<BaseResponse> GrantUserRoleByEmail(string email, string roleName);
        Task<BaseResponse> RevokeUserRoles(string userId);
        Task<BaseResponse> RevokeUserRole(string userId, string roleName);

<<<<<<< Updated upstream

        Task<BaseResponseGeneric<ICollection<UsuarioResponseDto>>> GetAsync(string? descripcion, PaginationDto pagination);
=======
        //--------------------------------------------------------------------------------------------
        Task<BaseResponseGeneric<ICollection<UsuarioInfo>>> GetAsyncAll(string? userName, PaginationDto pagination);
>>>>>>> Stashed changes
    }
}
