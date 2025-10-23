namespace Goreu.API.Controllers
{
    [ApiController]
    [Route("api/aplicaciones")]
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AplicacionController :ControllerBase
    {
        private readonly IAplicacionService service;

        public AplicacionController(IAplicacionService _service)
        {
            this.service = _service;
        }

        [HttpPost]
        public async Task<IActionResult> Post(int idEntidad,[FromBody] AplicacionRequestDto dto)
        {
            var response = await service.AddAsync(idEntidad, dto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] AplicacionRequestDto dto)
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
        
        [HttpGet("peruser")]
        public async Task<IActionResult> Get(string idUser)
        {
            var result = await service.GetAsyncPerUser(idUser);

            return result.Success ? Ok(result) : StatusCode(500, result.ErrorMessage);
        }
        [HttpGet("byEntidad")]
        public async Task<IActionResult> GetAlbyEntidad(int idEntidad, [FromQuery] PaginationDto pagination)
        {
            var result = await service.GetAllbyEntidad(idEntidad,pagination);

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
