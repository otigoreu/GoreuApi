namespace Goreu.Services.Interface
{
    public interface IEntidadService : IServiceBase<EntidadRequestDto, EntidadResponseDto>
    {
        Task<BaseResponseGeneric<ICollection<EntidadResponseDto>>> GetAsync(string? descripcion = null, PaginationDto? pagination = null);

        Task<BaseResponseGeneric<EntidadResponseDto>> GetAsyncPerUser(string idUser);

        Task<BaseResponseGeneric<EntidadSingleResponseDto>> GetAsyncPerRol(string idRol);
    }
}
