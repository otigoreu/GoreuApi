namespace Goreu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EntidadAplicacionController : ControllerBase
    {
        private readonly IEntidadAplicacionService service;

        public EntidadAplicacionController(IEntidadAplicacionService _service)
        {
            this.service = _service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EntidadAplicacionRequestDto dto)
        {
            var response = await service.AddAsync(dto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] EntidadAplicacionRequestDto dto)
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


        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await service.GetAsync(id);

            if (!response.Success)
                return NotFound(response); // Usar NotFound mejora semánticamente el mensaje

            return Ok(response);
        }

        [HttpGet("entidad/{idEntidad}/aplicaciones")]
        public async Task<IActionResult> GetAplicacionesPaginadas(
            [FromRoute] int idEntidad,
            [FromQuery] string? search,
            [FromQuery] PaginationDto pagination)
        {
            var result = await service.GetAplicacionesConEstadoPorEntidadAsync(idEntidad, search ?? string.Empty, pagination);

            return result.Success ? Ok(result) : StatusCode(500, result.ErrorMessage);
        }

        [HttpGet("entidad/{idEntidad}/aplicacion/{idAplicacion}")]
        public async Task<IActionResult> Get(
            [FromRoute] int idEntidad,
            [FromRoute] int idAplicacion
            )
        {
            var result = await service.GetAsync(idEntidad, idAplicacion);

            return Ok(result);
            //return result.Success ? Ok(result) : StatusCode(500, result.ErrorMessage);
        }
    }
}
