namespace Goreu.API.Controllers
{
    [Route("api/entidades")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EntidadController : ControllerBase
    {
        private readonly IEntidadService service;

        public EntidadController(IEntidadService _service)
        {
            this.service = _service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EntidadRequestDto dto)
        {
            var response = await service.AddAsync(dto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] EntidadRequestDto dto)
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

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] string userId,
            [FromQuery] string rolId,
            [FromQuery] string? search,
            [FromQuery] PaginationDto? pagination = null)
        {
            var result = await service.GetAsync(userId, rolId, search ?? string.Empty, pagination);

            return result.Success ? Ok(result) : StatusCode(500, result.ErrorMessage);
        }

        [HttpGet("peruser")]
        public async Task<IActionResult> Get(string idUser)
        {
            var result = await service.GetAsyncPerUser(idUser);

            return result.Success ? Ok(result) : StatusCode(500, result.ErrorMessage);
        }

        [HttpGet("perrol")]
        public async Task<IActionResult> Getrol(string idRol)
        {
            var result = await service.GetAsyncPerRol(idRol);

            return result.Success ? Ok(result) : StatusCode(500, result.ErrorMessage);
        }
    }
}
