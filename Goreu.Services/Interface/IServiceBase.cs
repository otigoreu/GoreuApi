using Goreu.Dto.Response;

namespace Goreu.Services.Interface
{
    public interface IServiceBase<TRequest, TResponse>
    {
        Task<BaseResponseGeneric<int>> AddAsync(TRequest request);
        Task<BaseResponse> UpdateAsync(int id, TRequest request);
        Task<BaseResponse> DeleteAsync(int id);
        Task<BaseResponse> FinalizeAsync(int id);
        Task<BaseResponse> InitializeAsync(int id);
        Task<BaseResponseGeneric<TResponse>> GetAsync(int id);
    }
}
