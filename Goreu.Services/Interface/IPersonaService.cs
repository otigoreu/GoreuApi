namespace Goreu.Services.Interface
{
    public interface IPersonaService
    {
        Task<BaseResponseGeneric<ICollection<PersonaResponseDto>>> GetAsync(string? nombres, PaginationDto? pagination);
        //Task<BaseResponseGeneric<PersonaInfo>> GetAsyncBYEmail(string email);
        Task<BaseResponseGeneric<PersonaResponseDto>> GetAsync(int id);
        Task<BaseResponseGeneric<PersonaResponseDto>> GetByNumDocumentoAsync(string numdoc);
        Task<BaseResponseGeneric<int>> AddAsync(PersonaRequestDto resquest);
        Task<BaseResponse> UpdateAsync(int id, PersonaRequestDto resquest);
        Task<BaseResponse> DeleteAsync(int id);
        Task<BaseResponse> FinalizedAsync(int id);
        Task<BaseResponse> InitializedAsync(int id);
    }
}
