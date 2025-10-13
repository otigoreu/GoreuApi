namespace Goreu.Services.Interface
{
    public interface IUsuarioUnidadOrganicaService : IServiceBase<UsuarioUnidadOrganicaRequestDto, UsuarioUnidadOrganicaResponseDto>
    {
        Task<BaseResponseGeneric<ICollection<UsuarioUnidadOrganicaResponseDto>>> GetUsuariosConEstadoPorUnidadorganicaAsync(int idUnidadorganica, string descripcion, PaginationDto pagination);
        Task<BaseResponseGeneric<ICollection<UsuarioUnidadOrganica_UnidadOrganicaResponseDto>>> GetUnidadOrganicasConEstado_ByUsuarioAsync(int idEntidad, string search, PaginationDto pagination, string userId);
        Task<BaseResponseGeneric<UsuarioUnidadOrganicaResponseDto>> GetAsync(int idUnidadOrganica, string idUsuario);
        Task<BaseResponseGeneric<ICollection<UnidadOrganicaResponseDto>>> GetUnidadOrganicasAsync(string idUsuario);
        Task<BaseResponse> FinalizeAsync(int id, string observacionAnulacion);

        Task<BaseResponse> ValidarAsync(UsuarioUnidadOrganicaRequestDto request);
    }
}

