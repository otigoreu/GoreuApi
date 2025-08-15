namespace Goreu.API.Controllers
{
    [ApiController]
    [Route("api/tipoRoles")]
    public class TipoRolController : ControllerBase
    {
        private readonly ITipoRolService service;

        public TipoRolController(ITipoRolService _service)
        {
            this.service = _service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TipoRolRequestDto dto)
        {
            var response = await service.AddAsync(dto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] TipoRolRequestDto dto)
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

        [HttpGet("descripcion")]
        public async Task<IActionResult> Get([FromQuery] string? search, [FromQuery] PaginationDto pagination)
        {
            var result = await service.GetAsync(search ?? string.Empty, pagination);

            return result.Success ? Ok(result) : StatusCode(500, result.ErrorMessage);
        }

       

    }
}
