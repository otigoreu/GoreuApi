using Goreu.Dto.Request;
using Goreu.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Services.Interface
{
    public interface IUsuarioUnidadOrganicaService : IServiceBase<UsuarioUnidadOrganicaRequestDto, UsuarioUnidadOrganicaResponseDto>
    {
        Task<BaseResponseGeneric<ICollection<UsuarioUnidadOrganicaResponseDto>>> GetAsync(int idUnidadOrganica, PaginationDto pagination);
    }
}

