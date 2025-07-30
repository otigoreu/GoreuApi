namespace Goreu.API.Controllers
{
    [ApiController]
    [Route("api/personas")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PersonasController : ControllerBase
    {
        private readonly IPersonaService service;

        public PersonasController(IPersonaService service)
        {
            this.service = service;
        }

        [HttpGet("nombre")]
        public async Task<IActionResult> Get(string? nombres, [FromQuery] PaginationDto pagination)
        {
            var response = await service.GetAsync(nombres, pagination);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpGet("nombrefilter")]
        public async Task<IActionResult> Getfilter(string? nombres, [FromQuery] PaginationDto pagination)
        {
            var response = await service.GetAsyncfilter(nombres, pagination);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpGet("email")]
        public async Task<IActionResult> Get(string? email)
        {
            var response = await service.GetAsyncBYEmail(email);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await service.GetAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PersonaRequestDto personRequestDto)
        {
            var response = await service.AddAsync(personRequestDto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, PersonaRequestDto personRequestDto)
        {
            var response = await service.UpdateAsync(id, personRequestDto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await service.DeleteAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpDelete("finalized/{id:int}")]
        public async Task<IActionResult> PatchFinit(int id)
        {
            var response = await service.FinalizedAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("initialized/{id:int}")]
        public async Task<IActionResult> PatchInit(int id)
        {

            var response = await service.InitializedAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
