namespace Goreu.Services.Interface
{
    public interface IUserRoleService
    {
        Task<BaseResponseGeneric<ICollection<UsuarioRol_UsuarioResponseDto>>> GetUsuarioAsync(
             int idEntidad,
             int idAplicacion,
             string? rolId,
             string? search,
             PaginationDto? pagination);
        Task<BaseResponseGeneric<ICollection<RolConAsignacionDto>>> GetRolesConAsignacionAsync(int idEntidad, int idAplicacion, string userId, string? search, PaginationDto? pagination);
        Task<BaseResponse> AsignarRoleAsync(int idEntidad, int idAplicacion, string userId, string rolId, bool selected);
        Task<BaseResponse> FinalizeAsync(Guid id);
        Task<BaseResponse> InitializeAsync(Guid id);
    }
}
