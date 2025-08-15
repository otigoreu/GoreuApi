using Goreu.Dto.Request;
using Goreu.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Services.Interface
{
    public interface ITipoRolService: IServiceBase<TipoRolRequestDto, TipoRolResponseDto>
    {
        Task<BaseResponseGeneric<ICollection<TipoRolResponseDto>>> GetAsync(string? descripcion, PaginationDto pagination);
    }
}
