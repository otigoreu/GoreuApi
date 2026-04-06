using Goreu.API.Filters;
using Goreu.Services.Common;

namespace Goreu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UnidadOrganicaController : ControllerBase
    {
        private readonly IUnidadOrganicaService unidadOrganicaService;

        public UnidadOrganicaController(IUnidadOrganicaService _unidadOrganicaService)
        {
            this.unidadOrganicaService = _unidadOrganicaService;
        }

        [HttpPost]
        [UnidadOrganicaExists]
        public async Task<IActionResult> Post([FromBody] UnidadOrganicaRequestDto dto)
        {
            var response = await unidadOrganicaService.AddAsync(dto);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] UnidadOrganicaRequestDto dto)
        {
            var result = await unidadOrganicaService.UpdateAsync(id, dto);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await unidadOrganicaService.DeleteAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPatch("{id:int}/finalize")]
        public async Task<IActionResult> Finalize(int id)
        {
            var response = await unidadOrganicaService.FinalizeAsync(id);

            if (!response.Success)
                return NotFound(response); // o BadRequest según el motivo

            return Ok(response);
        }

        [HttpPatch("{id:int}/initialize")]
        public async Task<IActionResult> Initialize(int id)
        {
            var response = await unidadOrganicaService.InitializeAsync(id);

            if (!response.Success)
                return NotFound(response); // o BadRequest según el motivo

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await unidadOrganicaService.GetAsync(id);

            if (!response.Success)
                return NotFound(response); // Usar NotFound mejora semánticamente el mensaje

            return Ok(response);
        }

        [HttpGet("descripcion")]
        public async Task<IActionResult> Get([FromQuery] int idTipo, [FromQuery] int? idEntidad, [FromQuery] string? search, [FromQuery] PaginationDto pagination)
        {
            var result = idEntidad switch
            {
                null => await unidadOrganicaService.GetAsync(search ?? string.Empty, pagination),
                int id => await unidadOrganicaService.GetAsync(idTipo, id, search ?? string.Empty, pagination)
            };

            return result.Success ? Ok(result) : StatusCode(500, result.ErrorMessage);
        }

        [HttpGet("peruser")]
        public async Task<IActionResult> Get(string idUser)
        {
            var result = await unidadOrganicaService.GetAsyncPerUser(idUser);

            return result.Success ? Ok(result) : StatusCode(500, result.ErrorMessage);
        }

        /// <summary>
        /// Obtiene todas las unidades orgánicas descendientes (hijos, nietos, etc.)
        /// de una unidad orgánica específica identificada por su <paramref name="idUnidad"/>.
        /// </summary>
        /// <param name="idUnidad">Identificador único de la unidad orgánica padre.</param>
        /// <returns>
        /// Retorna un objeto <see cref="BaseResponseGeneric{T}"/> con el resultado de la operación:
        /// - <c>Success = true</c> y la lista de unidades orgánicas descendientes si la operación es exitosa.
        /// - <c>Success = false</c> con el mensaje de error si ocurre una excepción durante la ejecución.
        /// </returns>
        /// <remarks>
        /// Este método consulta recursivamente todas las relaciones jerárquicas de la unidad orgánica,
        /// devolviendo un conjunto plano con todos los niveles inferiores.
        /// </remarks>
        [HttpGet("{id}/descendientes")]
        public async Task<IActionResult> GetDescendientesJerarquico(int id)
        {
            var result = await unidadOrganicaService.GetDescendientesJerarquicoAsync(id);
            return Ok(result);
        }

        [HttpGet("tree/search")]
        public async Task<IActionResult> SearchTree([FromQuery] int idEntidad, [FromQuery] string? descripcion)
        {
            var response = await unidadOrganicaService
                .SearchTreeAsync(idEntidad, descripcion);

            return Ok(response);
        }

        [HttpGet("descendants-count")]
        public async Task<IActionResult> GetDescendantsCount(
            [FromQuery] int idEntidad,
            [FromQuery] int id)
        {
            var result = await unidadOrganicaService
                .CountDescendantsAsync(idEntidad, id);

            return Ok(result);
        }

        [HttpGet("tree/context")]
        public async Task<IActionResult> Get([FromQuery] int idEntidad, [FromQuery] int idUnidadOrganica, [FromQuery] TipoBusquedaArbol tipoBusqueda, [FromQuery] string? descripcion)
        {
            var result = await unidadOrganicaService.GetTreeByUnidadOrganicaAsync(
                idEntidad,
                idUnidadOrganica,
                tipoBusqueda,
                descripcion);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
