using Goreu.Dto.Request;
using Goreu.Dto.Response;
using Goreu.Entities.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Services.Interface
{
    public interface IPersonaService
    {
        Task<BaseResponseGeneric<ICollection<PersonaInfo>>> GetAsync(string? nombres, PaginationDto pagination);
        Task<BaseResponseGeneric<ICollection<PersonaInfo>>> GetAsyncfilter(string? nombres, PaginationDto pagination);
        Task<BaseResponseGeneric<PersonaInfo>> GetAsyncBYEmail(string email);
        Task<BaseResponseGeneric<PersonaResponseDto>> GetAsync(int id);
        Task<BaseResponseGeneric<PersonaResponseDto>> GetAsyncNumdoc(string numdoc);
        Task<BaseResponseGeneric<int>> AddAsync(PersonaRequestDto resquest);
        Task<BaseResponse> UpdateAsync(int id, PersonaRequestDto resquest);
        Task<BaseResponse> DeleteAsync(int id);
        Task<BaseResponse> FinalizedAsync(int id);
        Task<BaseResponse> InitializedAsync(int id);
    }
}
