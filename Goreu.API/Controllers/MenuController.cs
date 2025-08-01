namespace Goreu.API.Controllers
{
    [ApiController]
    [Route("api/menus")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService service;

        public MenuController(IMenuService service)
        {
            this.service = service;
        }
        [HttpGet("displayName")]
        public async Task<IActionResult> Get(string? displayName)
        {

            var response = await service.GetAsync(displayName);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpGet("displayNameWithRole")]
        public async Task<IActionResult> GetWithRole(string? displayName)
        {

            var response = await service.GetAsyncWithRole(displayName);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{idAplication:int}")]
        public async Task<IActionResult> GetByAplication(int idAplication)
        {
            var userName = HttpContext.User.Claims.First(p => p.Type == ClaimTypes.Name).Value;
            var response = await service.GetByAplicationAsync(idAplication, userName);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(MenuRequestDto request)
        {
            var response = await service.AddAsync(request);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpPost("single")]
        public async Task<IActionResult> Post(MenuRequestDtoSingle request)
        {
            var response = await service.AddAsyncSingle(request);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, MenuRequestDto menuRequestDto)
        {
            var response = await service.UpdateAsync(id, menuRequestDto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut("single")]
        public async Task<IActionResult> Put(int id, MenuRequestDtoSingle request)
        {
            var response = await service.UpdateAsyncSingle(id, request);
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
