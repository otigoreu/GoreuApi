namespace Goreu.Services.Implementation
{
    public class RolService : IRolService
    {
        private readonly RoleManager<Rol> rolManager;
        private readonly ILogger<RolService> logger;
        private readonly IConfiguration configuration;
        private readonly SignInManager<Rol> singInManager;
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;
        private readonly IRolRepository repository;
        private readonly IEntidadAplicacionRepository entidadAplicacionRepository;

        public RolService(
            RoleManager<Rol> rolManager,
            ILogger<RolService> logger,
            IConfiguration configuration,
            IMapper mapper,
            ApplicationDbContext context,
            IRolRepository repository,
            IEntidadAplicacionRepository entidadAplicacionRepository
            )
        {

            this.rolManager = rolManager;
            this.logger = logger;
            this.configuration = configuration;
            this.mapper = mapper;
            this.context = context;
            this.repository = repository;
            this.entidadAplicacionRepository = entidadAplicacionRepository;
        }

        //FUNCIONA
        public async Task<BaseResponseGeneric<string>> AddSync(RolRequestDto request)
        {
            var response = new BaseResponseGeneric<string>();

            try
            {
                // Verificar si ya existe un rol con el mismo nombre en la misma aplicación
                var existingRol = await repository.GetAsync(request.IdEntidadAplicacion, request.Name);
                if (existingRol != null)
                {
                    response.ErrorMessage = $"Ya existe un rol con el nombre '{request.Name}' en esta aplicación.";
                    return response;
                }

                // Crear nuevo rol
                var rolnew = new Rol
                {
                    Name = request.Name,
                    NormalizedName = request.Name.ToUpperInvariant(),
                    IdEntidadAplicacion = request.IdEntidadAplicacion
                };

                response.Data = await repository.AddAsync(rolnew);
                response.Success = true;

                return response;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al intentar registrar el rol. Por favor, inténtalo nuevamente.";
                logger.LogError(ex, "Error al registrar el rol: {Message}", ex.Message);
            }

            return response;
        }

        //FUNCIONA
        public async Task<BaseResponse> DeleteAsync(string id)
        {
            var response = new BaseResponse();
            try
            {
                await repository.DeleteAsync(id);
                response.Success = true;
            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Ocurrio un error al Eliminar los datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> FinalizedAsync(string id)
        {
            var response = new BaseResponse();
            try
            {
                await repository.FinalizedAsync(id);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al finalizar Datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        //FUNCIONA
        public async Task<BaseResponseGeneric<ICollection<RolResponseDto>>> GetAsync()
        {
            var response = new BaseResponseGeneric<ICollection<RolResponseDto>>();
            try
            {
                var data = await repository.GetAllAsync();
                response.Data = mapper.Map<ICollection<RolResponseDto>>(data);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener los datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        //FUNCIONA
        public async Task<BaseResponseGeneric<RolResponseDto>> GetAsync(string id)
        {
            var response = new BaseResponseGeneric<RolResponseDto>();
            try
            {
                var data = await repository.GetAsync(id);
                response.Data = mapper.Map<RolResponseDto>(data);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener los datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<RolResponseSingleDto>>> GetAsyncPerUser(string idUser)
        {
            var response = new BaseResponseGeneric<ICollection<RolResponseSingleDto>>();
            try
            {
                var data = await repository.GetAsyncPerUser(idUser);
                response.Data = mapper.Map<ICollection<RolResponseSingleDto>>(data);
                response.Success = true;

            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Ocurrio un error al obtener los datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> InitializedAsync(string id)
        {
            var response = new BaseResponse();
            try
            {
                await repository.InitializedAsync(id);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al Inicializar Datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        //FUNCIONA
        public async Task<BaseResponse> UpdateAsync(string id, RolRequestDto request)
        {
            var response = new BaseResponse();

            try
            {
                var data = await rolManager.FindByIdAsync(id);
                if (data is null)
                {
                    response.ErrorMessage = $"El Rol con id {id} no fue encontrado";
                }


                data.Name = request.Name;
                data.NormalizedName = request.Name;
                data.IdEntidadAplicacion = request.IdEntidadAplicacion;
         
                await repository.UpdateAsync();
                response.Success = true;

            }
            catch (Exception ex)
            {

                response.ErrorMessage = "";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<RolPaginationResponseDto>>> GetAsync(
            int idEntidad,
            int idAplicacion,
            string? search,
            PaginationDto? pagination)
        {
            var response = new BaseResponseGeneric<ICollection<RolPaginationResponseDto>>();

            try
            {
                ICollection<Rol> data = await repository.GetAsync(idEntidad, idAplicacion, search, pagination);

                response.Data = mapper.Map<ICollection<RolPaginationResponseDto>>(data);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al listar los roles para la entidad y aplicacion.";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<RolEntidadAplicacionInfo>>> GetWithAllEntidadAplicacionAsync(int idEntidad, int idAplicacion)
        {
            var response = new BaseResponseGeneric<ICollection<RolEntidadAplicacionInfo>>();
            try
            {

                response.Data = await repository.GetWithAllEntidadAplicacionAsync(idEntidad, idAplicacion);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener los datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<RolEntidadAplicacionCounterInfo>>> GetWithAllEntidadAplicacionCounterAsync(int idEntidad, int idAplicacion)
        {
            var response = new BaseResponseGeneric<ICollection<RolEntidadAplicacionCounterInfo>>();
            try
            {

                response.Data = await repository.GetWithAllEntidadAplicacionCounterAsync(idEntidad, idAplicacion);
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
