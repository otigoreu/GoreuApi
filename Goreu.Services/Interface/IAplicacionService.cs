using Goreu.Dto.Request;
using Goreu.Dto.Response;
using Goreu.Entities;
using Goreu.Entities.Info;
using Goreu.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Goreu.Services.Interface
{
    public interface IAplicacionService : IServiceBase<AplicacionRequestDto, AplicacionResponseDto>
    {
        Task<BaseResponseGeneric<ICollection<AplicacionResponseDto>>> GetAsync(string? descripcion, PaginationDto pagination);

        Task<BaseResponseGeneric<ICollection<AplicacionResponseDto>>> GetAsyncPerUser(string idUser);
        Task<BaseResponseGeneric<AplicacionResponseDto>> GetAsyncPerRol(string idRol);

    }
}
