namespace Goreu.Services.Implementation
{
    public class AplicacionService : ServiceBase<Aplicacion, AplicacionRequestDto, AplicacionResponseDto>, IAplicacionService
    {
        private readonly IAplicacionRepository repository;
        private readonly IEntidadAplicacionRepository entidadAplicaiconRepository;

        public AplicacionService(
            IAplicacionRepository repository, 
            ILogger<AplicacionService> logger, 
            IMapper mapper, IEntidadAplicacionRepository entidadAplicaiconRepository) : base(repository, logger, mapper)
        {
           this.repository=repository;
            this.entidadAplicaiconRepository = entidadAplicaiconRepository;
           
        }

        public async Task<BaseResponseGeneric<int>> AddAsync(int idEntidad, AplicacionRequestDto request)
        {
            var response =new BaseResponseGeneric<int>();
            try
            {
               
                var app = await repository.AddAsync(mapper.Map<Aplicacion>(request));

                var ea = new EntidadAplicacionRequestDto {
                    idEntidad = idEntidad,
                    idAplicacion = app,
                };

                var entidaAplicaicon = await entidadAplicaiconRepository.AddAsync(mapper.Map<EntidadAplicacion>(ea));

                if (entidaAplicaicon !=0) {

                    response.Data = app;
                    response.Success = true;
                }

            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Ocurrio un error al crear la aplicacion";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<AplicacionResponseDto>>> GetAllbyEntidad(int idEntidad, PaginationDto pagination)
        {
            var response = new BaseResponseGeneric<ICollection<AplicacionResponseDto>>();

            try
            {
                var data = await repository.GetAllbyEntidad(idEntidad,orderBy: x => x.Descripcion,pagination);
                response.Data = mapper.Map<ICollection<AplicacionResponseDto>>(data);
                response.Success = true;

            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Ocurrio un error al obtener los datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<AplicacionResponseDto>>> GetAsync(string descripcion, PaginationDto pagination)
        {
            var response = new BaseResponseGeneric<ICollection<AplicacionResponseDto>>();
            try
            {
                var data = await repository.GetAsync(
                    predicate: s => s.Descripcion.Contains(descripcion ?? string.Empty),
                    orderBy: x => x.Descripcion,
                    pagination);

                response.Data = mapper.Map<ICollection<AplicacionResponseDto>>(data);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al filtrar las unidades organicas por descripción.";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponseGeneric<AplicacionResponseDto>> GetAsyncPerRol(string idRol)
        {
            var response = new BaseResponseGeneric<AplicacionResponseDto>();

            try
            {
                var data = await repository.GetAsyncPerRol(idRol);
                response.Data = mapper.Map<AplicacionResponseDto>(data);
                response.Success = true;

            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Ocurrio un error al obtener los datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<AplicacionResponseDto>>> GetAsyncPerUser(string idUser)
        {
            var response = new BaseResponseGeneric<ICollection<AplicacionResponseDto>>();

            try
            {
                var data = await repository.GetAsyncPerUser(idUser);
                response.Data = mapper.Map<ICollection<AplicacionResponseDto>>(data);
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
