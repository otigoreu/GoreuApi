namespace Goreu.Services.Implementation
{
    public class UsuarioUnidadOrganicaService : ServiceBase<UsuarioUnidadOrganica, UsuarioUnidadOrganicaRequestDto, UsuarioUnidadOrganicaResponseDto>, IUsuarioUnidadOrganicaService
    {
        private readonly IUsuarioUnidadOrganicaRepository repository;
        private readonly IUserService serviceUsuario;
        private readonly IUnidadOrganicaService serviceUnidadorganica;
        private readonly IRolRepository rolRepository;

        public UsuarioUnidadOrganicaService(IUsuarioUnidadOrganicaRepository repository, IUserService serviceUsuario, IUnidadOrganicaService serviceUnidadorganica, IRolRepository rolRepository, ILogger<UsuarioUnidadOrganicaService> logger, IMapper mapper) : base(repository, logger, mapper)
        {
            this.repository = repository; // ✅ Asignación correcta
            this.serviceUsuario = serviceUsuario;
            this.serviceUnidadorganica = serviceUnidadorganica;
            this.rolRepository = rolRepository;
        }

        public async Task<BaseResponseGeneric<ICollection<UsuarioUnidadOrganicaResponseDto>>> GetUsuariosConEstadoPorUnidadorganicaAsync(int idUnidadorganica, string descripcion, PaginationDto pagination)
        {
            var response = new BaseResponseGeneric<ICollection<UsuarioUnidadOrganicaResponseDto>>();

            try
            {
                // Paso 1: Obtener todas las usuarios
                var todasLosUsuarios = await serviceUsuario.GetAsync("definir", descripcion, pagination);

                // Paso 2: Obtener las aplicaciones asociadas a la entidad
                var usuariosUniadorganica = await repository.GetAsync(ea => ea.IdUnidadOrganica == idUnidadorganica);

                // Paso 3: Hacer join izquierdo entre todas las aplicaciones y las aplicaciones asociadas
                var resultado = todasLosUsuarios
                    .Data
                    .Select(app =>
                    {
                        var asociada = usuariosUniadorganica.FirstOrDefault(ea => ea.IdUsuario == app.Id);

                        return new UsuarioUnidadOrganicaResponseDto
                        {
                            Id = asociada?.Id ?? 0, // Si no está asociada, Id = 0
                            IdUnidadOrganica = asociada?.IdUnidadOrganica ?? 0, // Si no está asociada, IdEntidad = 0
                            IdUsuario = app.Id,
                            Numdoc = app.UserName,
                            DescripcionPersona = app.DescripcionPersona,
                            Estado = asociada?.Estado ?? false // Si no está asociada, se asume Estado = false
                        };
                    })
                    .OrderBy(x => x.DescripcionPersona)
                    .ToList();

                response.Data = resultado;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al listar los usuarios habilitadas para la unidad organica.";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<UsuarioUnidadOrganica_UnidadOrganicaResponseDto>>> GetUnidadOrganicasConEstado_ByUsuarioAsync(int idEntidad, string search, PaginationDto pagination, string userId)
        {
            var response = new BaseResponseGeneric<ICollection<UsuarioUnidadOrganica_UnidadOrganicaResponseDto>>();

            try
            {
                // Paso 2: Obtener las unidadesorganicas asociadas a al usuario
                var data = await repository.GetAsync(
                    predicate: z => z.UnidadOrganica.IdEntidad == idEntidad && z.IdUsuario == userId,
                    orderBy: z => z.UnidadOrganica.Descripcion,
                    pagination);

                response.Data = mapper.Map<ICollection<UsuarioUnidadOrganica_UnidadOrganicaResponseDto>>(data);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al listar los usuarios habilitadas para la unidad organica.";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        public async Task<BaseResponseGeneric<UsuarioUnidadOrganicaResponseDto>> GetAsync(int idUnidadOrganica, string idUsuario)
        {
            var response = new BaseResponseGeneric<UsuarioUnidadOrganicaResponseDto>();
            try
            {
                var entity = await repository.GetAsync(idUnidadOrganica, idUsuario);
                if (entity is null)
                {
                    response.ErrorMessage = $"La entidad con ID {idUnidadOrganica} y la aplicacion con ID {idUsuario} no existe.";
                    return response;
                }

                response.Data = mapper.Map<UsuarioUnidadOrganicaResponseDto>(entity);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al obtener la entidad.";
                logger.LogError(ex, "{ErrorMessage} {Exception}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<UnidadOrganicaResponseDto>>> GetUnidadOrganicasAsync(string userId)
        {
            var response = new BaseResponseGeneric<ICollection<UnidadOrganicaResponseDto>>();

            try
            {
                var unidadesOrganicas = await repository.GetAsync(
                    predicate: z => z.IdUsuario == userId && z.Usuario.Estado,
                    orderBy: z => z.UnidadOrganica.Descripcion,
                    pagination: null // no paginar aquí
                );

                // Solo mapear la parte de UnidadOrganica
                var data = unidadesOrganicas
                    .Select(u => u.UnidadOrganica)
                    .ToList();

                response.Data = mapper.Map<ICollection<UnidadOrganicaResponseDto>>(data);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al listar las unidades orgánicas habilitadas para el usuario.";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse> FinalizeAsync(int id, string observacionAnulacion)
        {
            var response = new BaseResponse();

            try
            {
                await repository.FinalizeAsync(id, observacionAnulacion);

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = $"Error al finalizar la Unidad Orgánica de Usuario con ID {id}.";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }
    }

}
