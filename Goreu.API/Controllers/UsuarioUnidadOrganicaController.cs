namespace Goreu.API.Controllers
{
    [Route("api/usuarioUnidadOrganicas")]
    [ApiController]
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsuarioUnidadOrganicaController : ControllerBase
    {
        private readonly IUsuarioUnidadOrganicaService service;

        public UsuarioUnidadOrganicaController(IUsuarioUnidadOrganicaService _service)
        {
            this.service = _service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UsuarioUnidadOrganicaRequestDto dto)
        {
            var response = await service.AddAsync(dto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] UsuarioUnidadOrganicaRequestDto dto)
        {
            var result = await service.UpdateAsync(id, dto);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await service.DeleteAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPatch("{id:int}/finalize")]
        public async Task<IActionResult> Finalize(int id)
        {
            var response = await service.FinalizeAsync(id);

            if (!response.Success)
                return NotFound(response); // o BadRequest según el motivo

            return Ok(response);
        }

        [HttpPatch("{id:int}/initialize")]
        public async Task<IActionResult> Initialize(int id)
        {
            var response = await service.InitializeAsync(id);

            if (!response.Success)
                return NotFound(response); // o BadRequest según el motivo

            return Ok(response);
        }


        /// <summary>
        /// Obtiene la información de una Unidad Orgánica de Usuario específica por su identificador único.
        /// </summary>
        /// <param name="id">Identificador único de la Unidad Orgánica de Usuario.</param>
        /// <returns>
        /// Retorna un objeto con el resultado de la operación:
        /// - <c>200 OK</c> con los datos de la Unidad Orgánica de Usuario si se encuentra el registro.
        /// - <c>404 Not Found</c> si no existe una Unidad Orgánica de Usuario con el <paramref name="id"/> especificado.
        /// - <c>500 Internal Server Error</c> si ocurre un error inesperado durante el procesamiento.
        /// </returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await service.GetAsync(id);

            if (!response.Success)
                return NotFound(response); // Usar NotFound mejora semánticamente el mensaje

            return Ok(response);
        }


        [HttpGet("unidadorganica/{idUnidadorganica}/usuarios")]
        [AllowAnonymous] // -----------------------------------------------------------------------------------------------------------> BORRAR
        public async Task<IActionResult> GetUsuariosPaginadas(
            [FromRoute] int idUnidadorganica,
            [FromQuery] string? search,
            [FromQuery] PaginationDto pagination)
        {
            var result = await service.GetUsuariosConEstadoPorUnidadorganicaAsync(idUnidadorganica, search ?? string.Empty, pagination);

            return result.Success ? Ok(result) : StatusCode(500, result.ErrorMessage);
        }

        /// <summary>
        /// Devuelve una lista paginada de Unidades Orgánicas asociadas a un usuario.
        /// </summary>
        /// <param name="idEntidad">Identificador de la entidad a la que pertenece el usuario.</param>
        /// <param name="search">Texto opcional para filtrar por nombre o descripción de la Unidad Orgánica.</param>
        /// <param name="pagination">Parámetros de paginación (número de página y tamaño de página).</param>
        /// <param name="userId">Identificador único del usuario.</param>
        /// <returns>
        /// Retorna un objeto con el resultado de la operación:
        /// - <c>200 OK</c> con la lista de Unidades Orgánicas asociadas si la operación es exitosa.
        /// - <c>500 Internal Server Error</c> si ocurre un error durante el procesamiento.
        /// </returns>
        [HttpGet("usuario/{userId}/unidadorganicas")]
        public async Task<IActionResult> GetUnidadOrganicasPaginadas(
            [FromQuery] int idEntidad,
            [FromQuery] string? search,
            [FromQuery] PaginationDto pagination,
            [FromRoute] string userId)
        {
            var result = await service.GetUnidadOrganicasConEstado_ByUsuarioAsync(idEntidad, search ?? string.Empty, pagination, userId);

            return result.Success ? Ok(result) : StatusCode(500, result.ErrorMessage);
        }

        [HttpGet("unidadorganica/{idUnidadorganica}/usuario/{idusuario}")]
        public async Task<IActionResult> Get(
            [FromRoute] int idUnidadorganica,
            [FromRoute] string idusuario
            )
        {
            var result = await service.GetAsync(idUnidadorganica, idusuario);

            return Ok(result);
            //return result.Success ? Ok(result) : StatusCode(500, result.ErrorMessage);
        }

        
    }
}
