using AutoMapper;
using Goreu.Dto.Request;
using Goreu.Dto.Response;
using Goreu.Entities;
using Goreu.Repositories.Interface;
using Goreu.Services.Interface;
using Microsoft.Extensions.Logging;

namespace Goreu.Services.Implementation
{
    public class UnidadOrganicaService : ServiceBase<UnidadOrganica, UnidadOrganicaRequestDto, UnidadOrganicaResponseDto>, IUnidadOrganicaService
    {
        //private readonly IUnidadOrganicaRepository repository;
        //private readonly ILogger<UnidadOrganicaService> logger;
        //private readonly IMapper mapper;

        //public UnidadOrganicaService(IUnidadOrganicaRepository repository, ILogger<UnidadOrganicaService> logger, IMapper mapper)
        //{
        //    this.repository = repository;
        //    this.logger = logger;
        //    this.mapper = mapper;
        //}

        private readonly IUnidadOrganicaRepository repository;

        public UnidadOrganicaService(IUnidadOrganicaRepository repository, ILogger<UnidadOrganicaService> logger, IMapper mapper) : base(repository, logger, mapper)
        {
            this.repository = repository; // ✅ Asignación correcta
        }

        public async Task<BaseResponseGeneric<ICollection<UnidadOrganicaResponseDto>>> GetAsync(string descripcion, PaginationDto pagination)
        {
            var response = new BaseResponseGeneric<ICollection<UnidadOrganicaResponseDto>>();
            try
            {
                var data = await repository.GetAsync(
                    predicate: s => s.Descripcion.Contains(descripcion ?? string.Empty),
                    orderBy: x => x.Descripcion,
                    pagination);

                response.Data = mapper.Map<ICollection<UnidadOrganicaResponseDto>>(data);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al filtrar las unidades organicas por descripción.";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<UnidadOrganicaResponseDto>>> GetAsync(int idEntidad, string descripcion, PaginationDto pagination)
        {
            var response = new BaseResponseGeneric<ICollection<UnidadOrganicaResponseDto>>();
            try
            {
                var data = await repository.GetAsync(
                    predicate: s => s.IdEntidad == idEntidad && s.Descripcion.Contains(descripcion ?? string.Empty),
                    orderBy: x => x.Descripcion,
                    pagination);

                response.Data = mapper.Map<ICollection<UnidadOrganicaResponseDto>>(data);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al filtrar las unidades organicas por descripción.";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<UnidadOrganicaResponseSingleDto>>> GetAsyncPerUser(string idUser)
        {
            var response = new BaseResponseGeneric<ICollection<UnidadOrganicaResponseSingleDto>>();

            try
            {
                var data = await repository.GetAsyncPerUser(idUser);
                response.Data = mapper.Map<ICollection<UnidadOrganicaResponseSingleDto>>(data);
                response.Success = true;

            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Ocurrio un error al obtener los datos";
                logger.LogError(ex,"{ErrorMessage} {Message}", response.ErrorMessage,ex.Message);
            }

            return response;
        }
    }

}
