using Goreu.Dto.Response;
using Goreu.DtoResponse;

namespace Goreu.API.Controllers
{
    [ApiController]
    [Route("api/roles")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RolController : ControllerBase
    {
        private readonly IRolService service;

        public RolController(IRolService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await service.GetAsync();
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpGet("Withentidadaplicacion")]
        public async Task<IActionResult> GetWithEntidadAplicacion(int idEntidad,int idAplicacion)
        {
            var response = await service.GetWithAllEntidadAplicacionAsync(idEntidad,idAplicacion);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpGet("WithentidadaplicacionCounter")]
        public async Task<IActionResult> GetWithEntidadAplicacionCounter(int idEntidad, int idAplicacion)
        {
            var response = await service.GetWithAllEntidadAplicacionCounterAsync(idEntidad, idAplicacion);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpGet("Counter")]
        public async Task<IActionResult> GetCounter(int idEntidad, int idAplicacion)
        {
            var response = await service.GetCounterAsync(idEntidad, idAplicacion);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("id")]
        public async Task<IActionResult> Get(string id)
        {
            var response = await service.GetAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(RolRequestDto rolRequestDto)
        {
            var response = await service.AddSync(rolRequestDto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(string id, RolRequestDto rolRequestDto)
        {
            var response = await service.UpdateAsync(id, rolRequestDto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await service.DeleteAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("finalized")]
        public async Task<IActionResult> PatchFinit(string id)
        {

            var response = await service.FinalizedAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("initialized")]
        public async Task<IActionResult> PatchInit(string id)
        {

            var response = await service.InitializedAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("peruser")]
        public async Task<IActionResult> GetPerUser(string idUser)
        {
            var result = await service.GetAsyncPerUser(idUser);

            return result.Success ? Ok(result) : StatusCode(500, result.ErrorMessage);
        }

        /// <summary>
        /// Obtiene la lista de roles asociados a una entidad y aplicación, 
        /// con soporte de búsqueda y paginación.
        /// </summary>
        /// <param name="idEntidad">Identificador único de la entidad.</param>
        /// <param name="idAplicacion">Identificador único de la aplicación.</param>
        /// <param name="search">Texto opcional para filtrar resultados.</param>
        /// <param name="pagination">Objeto opcional con parámetros de paginación (página, tamaño, etc.).</param>
        /// <param name="rolId">Identificador opcional del rol para filtrar resultados.</param>
        /// <returns>
        /// Un resultado HTTP con los siguientes posibles códigos de estado:
        /// - <c>200 OK</c>: Devuelve la lista de roles paginada y filtrada.
        /// - <c>400 Bad Request</c>: Si hubo un error en los parámetros de entrada o en la operación.
        /// - <c>500 Internal Server Error</c>: Si ocurrió un error inesperado en el servidor.
        /// </returns>
        [HttpGet("entidad/{idEntidad}/Aplicacion/{idAplicacion}")]
        //[ProducesResponseType(typeof(BaseResponseGeneric<ICollection<RolPaginationResponseDto>>), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(BaseResponseGeneric<ICollection<RolPaginationResponseDto>>), StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(
            [FromRoute] int idEntidad,
            [FromRoute] int idAplicacion,
            [FromQuery] string? search,
            [FromQuery] PaginationDto? pagination
            )
        {
            var response = await service.GetAsync(idEntidad, idAplicacion, search, pagination);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
