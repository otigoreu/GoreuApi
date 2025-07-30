namespace Goreu.API.Controllers
{
    [ApiController]
    [Route("api/roles")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RolController : ControllerBase
    {

        private readonly IRolService service;

        public RolController(IRolService service)
        {
            this.service = service;
        }

        [HttpGet]
        [AllowAnonymous] // -----------------------------------------------------------------------------------------------------------> BORRAR
        public async Task<IActionResult> Get()
        {
            var response = await service.GetAsync();
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
    }
}
