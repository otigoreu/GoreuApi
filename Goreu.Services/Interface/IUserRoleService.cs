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

        Task<BaseResponse> FinalizeAsync(Guid id);

        Task<BaseResponse> InitializeAsync(Guid id);
    }
}
