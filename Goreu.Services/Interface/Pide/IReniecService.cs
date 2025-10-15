using Goreu.Dto.Request.Pide;

namespace Goreu.Services.Interface.Pide
{
    public interface IReniecService
    {
        Task<ReniecResponseModel> ConsultarPersonaAsync(GetReniecRequest request);
    }
}
