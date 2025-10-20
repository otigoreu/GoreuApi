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

            var response = await userRoleService.GetUsuarioAsync(idEntidad, idAplicacion, search, pagination);

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
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
