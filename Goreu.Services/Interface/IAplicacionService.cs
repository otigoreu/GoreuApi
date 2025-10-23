namespace Goreu.Services.Interface
{
    public interface IAplicacionService : IServiceBase<AplicacionRequestDto, AplicacionResponseDto>
    {
        Task<BaseResponseGeneric<int>> AddAsync(int idEntidad,AplicacionRequestDto request);
        Task<BaseResponseGeneric<ICollection<AplicacionResponseDto>>> GetAsync(string? descripcion, PaginationDto pagination);
        Task<BaseResponseGeneric<ICollection<AplicacionResponseDto>>> GetAllbyEntidad(int idEntidad, PaginationDto pagination);

        Task<BaseResponseGeneric<ICollection<AplicacionResponseDto>>> GetAsyncPerUser(string idUser);
        Task<BaseResponseGeneric<AplicacionResponseDto>> GetAsyncPerRol(string idRol);

    }
}
