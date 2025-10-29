namespace Goreu.Services.Implementation
{
    public class UserRoleService : IUserRoleService
    {
        private readonly ILogger<UserRoleService> logger;
        private readonly IUserRoleRepository userRolRepository;
        private readonly IMapper mapper;

        public UserRoleService(
            ILogger<UserRoleService> logger,
            IUserRoleRepository userRolRepository,
            IMapper mapper)
        {
            this.logger = logger;
            this.userRolRepository = userRolRepository;
            this.mapper = mapper;
        }

        public async Task<BaseResponseGeneric<ICollection<UsuarioRol_UsuarioResponseDto>>> GetUsuarioAsync(
            int idEntidad,
            int idAplicacion,
            string? rolId,
            string? search,
            PaginationDto? pagination)
        {
            var response = new BaseResponseGeneric<ICollection<UsuarioRol_UsuarioResponseDto>>();

            try
            {
                search = string.IsNullOrWhiteSpace(search) ? "" : search;

                var data = await userRolRepository.GetUsuarioAsync(idEntidad, idAplicacion, rolId, search, pagination);

                response.Data = mapper.Map<ICollection<UsuarioRol_UsuarioResponseDto>>(data);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al obtener usuarios.";
                logger.LogError(ex, "{ErrorMessage}. Parámetros -> search: {Search}", response.ErrorMessage, search);
            }

            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<RolConAsignacionDto>>> GetRolesConAsignacionAsync(int idEntidad, int idAplicacion, string userId, string? search, PaginationDto? pagination)
        {
            var response = new BaseResponseGeneric<ICollection<RolConAsignacionDto>>();

            try
            {
                var data = await userRolRepository.GetRolesConAsignacionAsync(idEntidad, idAplicacion, userId);

                response.Data = data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al obtener los roles del usuario.";
                logger.LogError(ex, "{ErrorMessage}. Parámetros -> search: {Search}", response.ErrorMessage, search);
            }

            return response;
        }

        public async Task<BaseResponse> AsignarRoleAsync(int idEntidad, int idAplicacion, string userId, string rolId, bool selected)
        {
            var response = new BaseResponse();
            try
            {
                await userRolRepository.AsignarRoleAsync(idEntidad, idAplicacion, userId, rolId, selected);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al Asignar rol al usuario.";
                logger.LogError(ex, "{ErrorMessage} {Exception}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> FinalizeAsync(Guid id)
        {
            var response = new BaseResponse();
            try
            {
                await userRolRepository.FinalizeAsync(id);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al finalizar el usuario.";
                logger.LogError(ex, "{ErrorMessage} {Exception}", response.ErrorMessage, ex.Message);
            }
            return response;
        }



        public async Task<BaseResponse> InitializeAsync(Guid id)
        {
            var response = new BaseResponse();
            try
            {
                await userRolRepository.InitializeAsync(id);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al inicializar el usuario.";
                logger.LogError(ex, "{ErrorMessage} {Exception}", response.ErrorMessage, ex.Message);
            }
            return response;
        }
    }
}