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


        /// <summary>
        /// Obtiene las unidades orgánicas descendientes en formato jerárquico.
        /// </summary>
        /// <param name="idUnidad">Identificador de la unidad padre.</param>
        /// <returns>Retorna las unidades hijas con estructura jerárquica.</returns>
        public async Task<BaseResponseGeneric<ICollection<UnidadOrganicaResponseDto>>> GetDescendientesJerarquicoAsync(int idUnidadOrganica)
        {
            var response = new BaseResponseGeneric<ICollection<UnidadOrganicaResponseDto>>();

            try
            {
                var descendientes = await repository.ObtenerDescendientesAsync(idUnidadOrganica);

                // Mapeamos a DTOs planos
                var descendientesDto = mapper.Map<List<UnidadOrganicaResponseDto>>(descendientes);

                // Construimos la jerarquía
                var jerarquico = ConstruirJerarquia(descendientesDto, idUnidadOrganica);

                response.Data = jerarquico;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al construir la jerarquía de unidades orgánicas.";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        /// <summary>
        /// Construye una estructura jerárquica a partir de una lista plana.
        /// </summary>
        private List<UnidadOrganicaResponseDto> ConstruirJerarquia(List<UnidadOrganicaResponseDto> lista, int? idPadre)
        {
            return lista
                .Where(x => x.idDependencia == idPadre)
                .Select(x => new UnidadOrganicaResponseDto
                {
                    Id = x.Id,
                    Descripcion = x.Descripcion,
                    Abrev = x.Abrev,
                    idEntidad = x.idEntidad,
                    idDependencia = x.idDependencia,
                    Hijos = ConstruirJerarquia(lista, x.Id) // 👈 Recursión para los hijos
                })
                .ToList();
        }
    }

}
