using AutoMapper;
using Goreu.Dto;
using Goreu.Dto.Request;
using Goreu.Dto.Response;
using Goreu.Entities;
using Goreu.Repositories.Interface;
using Goreu.Services.Interface;
using Microsoft.Extensions.Logging;

namespace Goreu.Services.Implementation
{
    public class EntidadService : ServiceBase<Entidad, EntidadRequestDto, EntidadResponseDto>, IEntidadService
    {
        private readonly IEntidadRepository repository;

        public EntidadService(IEntidadRepository repository, ILogger<EntidadService> logger, IMapper mapper) : base(repository, logger, mapper)
        {
            this.repository = repository; // ✅ Asignación correcta
        }

        public async Task<BaseResponseGeneric<ICollection<EntidadResponseDto>>> GetAsync(string descripcion, PaginationDto pagination)
        {
            var response = new BaseResponseGeneric<ICollection<EntidadResponseDto>>();
            try
            {
                var data = await repository.GetAsync(
                    predicate: s => s.Descripcion.Contains(descripcion ?? string.Empty),
                    orderBy: x => x.Descripcion,
                    pagination);

                response.Data = mapper.Map<ICollection<EntidadResponseDto>>(data);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al filtrar las unidades organicas por descripción.";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponseGeneric<EntidadResponseDto>> GetAsyncPerUser(string idUser)
        {
            var response = new BaseResponseGeneric<EntidadResponseDto>();

            try
            {
                var data = await repository.GetAsyncPerUser(idUser);
                response.Data = mapper.Map<EntidadResponseDto>(data);
                response.Success = true;

            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Ocurrio un error al obtener los datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }
    }
}
