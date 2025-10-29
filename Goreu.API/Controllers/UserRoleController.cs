using Goreu.Dto.Response;

namespace Goreu.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService userRoleService;
        private readonly IConfiguration configuration;

        public UserRoleController(IUserRoleService userRoleService, IConfiguration configuration)
        {
            this.userRoleService = userRoleService;
            this.configuration = configuration;
        }

        /// <summary>
        /// Obtiene la lista paginada de usuarios asociados a una entidad y aplicación específica.
        /// </summary>
        /// <param name="idEntidad">
        /// Identificador único de la entidad a la que pertenecen los usuarios.
        /// </param>
        /// <param name="idAplicacion">
        /// Identificador único de la aplicación dentro de la entidad.
        /// </param>
        /// <param name="search">
        /// Texto opcional de búsqueda para filtrar los usuarios por nombre, apellidos o nombre de usuario.
        /// </param>
        /// <param name="pagination">
        /// Parámetros opcionales de paginación:
        /// <see cref="PaginationDto.Page"/> (número de página, por defecto 1) y 
        /// <see cref="PaginationDto.RecordsPerPage"/> (registros por página, por defecto 50).
        /// </param>
        /// <returns>
        /// Retorna un resultado HTTP con la información de los usuarios filtrados:
        /// <list type="bullet">
        /// <item>
        /// <description><c>200 OK</c>: Si la consulta fue exitosa. Devuelve un objeto <see cref="BaseResponseGeneric{T}"/> con una colección de <see cref="UsuarioRol_UsuarioResponseDto"/>.</description>
        /// </item>
        /// <item>
        /// <description><c>400 Bad Request</c>: Si los parámetros de entrada son inválidos o hubo un error de validación.</description>
        /// </item>
        /// <item>
        /// <description><c>500 Internal Server Error</c>: Si ocurre un error inesperado durante el procesamiento.</description>
        /// </item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// Este endpoint permite:
        /// 1. Filtrar usuarios por nombre, apellidos o nombre de usuario.
        /// 2. Paginar los resultados para optimizar el rendimiento en grandes volúmenes de datos.
        /// 3. Devuelve en el encabezado HTTP la cantidad total de registros mediante <c>totalrecordsquantity</c>.
        /// </remarks>
        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] int idEntidad,
            [FromQuery] int idAplicacion,
            [FromQuery] string? rolId,
            [FromQuery] string? search,
            [FromQuery] PaginationDto? pagination)
        {
            // Validación básica para evitar llamadas inválidas
            if (idEntidad <= 0 || idAplicacion <= 0)
            {
                return BadRequest(new
                {
                    Success = false,
                    ErrorMessage = "Los parámetros 'idEntidad' y 'idAplicacion' deben ser mayores a cero."
                });
            }

            var response = await userRoleService.GetUsuarioAsync(idEntidad, idAplicacion, rolId, search, pagination);

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        /// <summary>
        /// Obtiene la lista de roles disponibles en una entidad y aplicación, indicando cuáles están asignados a un usuario específico.
        /// </summary>
        /// <param name="idEntidad">
        /// Identificador único de la entidad sobre la cual se consultan los roles.
        /// </param>
        /// <param name="idAplicacion">
        /// Identificador único de la aplicación dentro de la entidad.
        /// </param>
        /// <param name="userId">
        /// Identificador único del usuario (Guid o string de Identity) para el cual se desea conocer los roles asignados.
        /// </param>
        /// <param name="search">
        /// Texto opcional para filtrar los roles por nombre o descripción. Si no se envía, devuelve todos los roles.
        /// </param>
        /// <param name="pagination">
        /// Parámetros opcionales de paginación:
        /// <see cref="PaginationDto.Page"/> (número de página, por defecto 1) y 
        /// <see cref="PaginationDto.RecordsPerPage"/> (registros por página, por defecto 50).
        /// </param>
        /// <returns>
        /// Retorna un resultado HTTP con la información de los roles y su estado de asignación al usuario:
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// <c>200 OK</c>: Si la consulta fue exitosa. Devuelve un objeto <see cref="BaseResponseGeneric{T}"/> con una colección de <see cref="RolConAsignacionDto"/>.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// <c>400 Bad Request</c>: Si los parámetros de entrada son inválidos o faltan datos requeridos.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// <c>500 Internal Server Error</c>: Si ocurre un error inesperado durante el procesamiento.
        /// </description>
        /// </item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// Este endpoint permite:
        /// 1. Obtener todos los roles disponibles en una entidad/aplicación.
        /// 2. Identificar de forma clara cuáles roles ya están asignados al usuario indicado.
        /// 3. Aplicar filtros y paginación para optimizar el rendimiento en consultas grandes.
        /// 4. Devuelve en el encabezado HTTP la cantidad total de registros mediante <c>totalrecordsquantity</c>.
        /// </remarks>
        [HttpGet("asignacion")]
        public async Task<IActionResult> GetRolesConAsignacion(
            [FromQuery] int idEntidad,
            [FromQuery] int idAplicacion,
            [FromQuery] string userId,
            [FromQuery] string? search,
            [FromQuery] PaginationDto? pagination)
        {
            // Validación básica para evitar llamadas inválidas
            if (idEntidad <= 0 || idAplicacion <= 0)
            {
                return BadRequest(new
                {
                    Success = false,
                    ErrorMessage = "Los parámetros 'idEntidad' y 'idAplicacion' deben ser mayores a cero."
                });
            }

            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest(new BaseResponseGeneric<object>
                {
                    Success = false,
                    ErrorMessage = "El parámetro 'userId' es obligatorio."
                });
            }

            // 🔹 Ejecución del servicio
            var response = await userRoleService.GetRolesConAsignacionAsync(idEntidad, idAplicacion, userId, search, pagination);

            // 🔹 Respuesta según resultado
            if (!response.Success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponseGeneric<object>
                {
                    Success = false,
                    ErrorMessage = response.ErrorMessage ?? "Ocurrió un error al obtener los roles asignados."
                });
            }

            // ✅ Éxito
            return Ok(response);
        }

        /// <summary>
        /// Asigna o quita un rol específico a un usuario dentro de una entidad y aplicación.
        /// </summary>
        /// <param name="request">
        /// Objeto de tipo <see cref="RolConAsignacionRequestDto"/> que contiene los datos necesarios para la operación:
        /// <list type="bullet">
        /// <item><description><c>idEntidad</c>: Identificador único de la entidad.</description></item>
        /// <item><description><c>idAplicacion</c>: Identificador único de la aplicación dentro de la entidad.</description></item>
        /// <item><description><c>userId</c>: Identificador único del usuario (Guid o string de Identity).</description></item>
        /// <item><description><c>rolId</c>: Identificador único del rol a asignar o desasignar.</description></item>
        /// <item><description><c>selected</c>: Valor booleano que indica la acción a realizar:
        /// <c>true</c> para asignar el rol, <c>false</c> para quitarlo.</description></item>
        /// </list>
        /// </param>
        /// <returns>
        /// Retorna un resultado HTTP que indica el resultado de la operación:
        /// <list type="bullet">
        /// <item>
        /// <description><c>200 OK</c>: Si la asignación o desasignación del rol se realizó correctamente. Devuelve un objeto <see cref="BaseResponse"/> con <c>Success = true</c>.</description>
        /// </item>
        /// <item>
        /// <description><c>404 Not Found</c>: Si el usuario o el rol no existen o no están relacionados con la entidad/aplicación indicada.</description>
        /// </item>
        /// <item>
        /// <description><c>400 Bad Request</c>: Si los parámetros son inválidos o faltan datos requeridos.</description>
        /// </item>
        /// <item>
        /// <description><c>500 Internal Server Error</c>: Si ocurre un error inesperado durante el procesamiento.</description>
        /// </item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// Este endpoint permite actualizar la relación entre usuarios y roles de forma dinámica:
        /// <list type="number">
        /// <item>Asignar un rol al usuario (cuando <c>selected = true</c>).</item>
        /// <item>Quitar un rol previamente asignado (cuando <c>selected = false</c>).</item>
        /// </list>
        /// No crea duplicados de asignaciones existentes.  
        /// En caso de que la relación ya exista, solo actualiza su estado (<c>Estado</c>).
        /// </remarks>

        [HttpPatch("asignar-rol")]
        public async Task<IActionResult> AsignarRoleAsync(RolConAsignacionRequestDto request)
        {
            var response = await userRoleService.AsignarRoleAsync(request.IdEntidad, request.IdAplicacion, request.UserId, request.RolId, request.Selected);

            if (!response.Success)
                return NotFound(response); // o BadRequest según el motivo

            return Ok(response);
        }

        [HttpPatch("{id}/finalize")]
        public async Task<IActionResult> Finalize(Guid id)
        {
            var response = await userRoleService.FinalizeAsync(id);

            if (!response.Success)
                return NotFound(response); // o BadRequest según el motivo

            return Ok(response);
        }

        [HttpPatch("{id}/initialize")]
        public async Task<IActionResult> Initialize(Guid id)
        {
            var response = await userRoleService.InitializeAsync(id);

            if (!response.Success)
                return NotFound(response); // o BadRequest según el motivo

            return Ok(response);
        }
    }
}
