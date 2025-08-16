using Goreu.Dto.Request;
using Goreu.Dto.Response;
using Goreu.DtoResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Services.Interface
{
    public interface IRolService
    {
        Task<BaseResponseGeneric<string>> AddSync(RolRequestDto request);
        Task<BaseResponse> DeleteAsync(string id);
        Task<BaseResponse> UpdateAsync(string id, RolRequestDto request);
        Task<BaseResponseGeneric<ICollection<RolResponseDto>>> GetAsync();
        Task<BaseResponseGeneric<RolResponseDto>> GetAsync(string id);
        Task<BaseResponseGeneric<ICollection<RolResponseSingleDto>>> GetAsyncPerUser(string idUser);
        Task<BaseResponse> FinalizedAsync(string id);
        Task<BaseResponse> InitializedAsync(string id);
    }
}
