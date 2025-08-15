using AutoMapper;
using Goreu.Dto.Request;
using Goreu.Dto.Response;
using Goreu.Entities;
using Goreu.Repositories.Interface;
using Goreu.Services.Interface;
using Microsoft.Extensions.Logging;

namespace Goreu.Services.Implementation
{
    public class TipoRolService : ServiceBase<TipoRol, TipoRolRequestDto, TipoRolResponseDto>, ITipoRolService
    {
        private readonly ITipoRolRepository repository;
        public TipoRolService(ITipoRolRepository repository, ILogger<TipoRolService> logger, IMapper mapper) : base(repository, logger, mapper)
        {
            this.repository = repository;
        }

        public async  Task<BaseResponseGeneric<ICollection<TipoRolResponseDto>>> GetAsync(string? descripcion, PaginationDto pagination)
        {
            var response = new BaseResponseGeneric<ICollection<TipoRolResponseDto>>();
            try
            {
                var data = await repository.GetAsync(
                    predicate:s=>s.Descripcion.Contains(descripcion ?? string.Empty),
                    orderBy:x=>x.Descripcion,
                    pagination);
                response.Data=mapper.Map<ICollection<TipoRolResponseDto>>(data);
                response.Success=true;
            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Error al filtrar los tipo de Rol";
                logger.LogError(ex,"{ErrorMessage}{Message}",response.ErrorMessage ,ex.Message);
            }
            return response;
        }
    }
}
