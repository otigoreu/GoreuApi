namespace Goreu.API.Controllers
{
    [ApiController]
    [Route("api/tipodocumentos")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TipoDocumentoController : ControllerBase
    {
        private readonly ITipoDocumentoService service;

        public TipoDocumentoController(ITipoDocumentoService service)
        {
            this.service = service;
        }

        [HttpGet("descripcion")]
        public async Task<IActionResult> Get(string? descripcion)
        {

            var response = await service.GetAsync(descripcion);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var response = await service.GetAsync();
            return response.Success ? Ok(response) : BadRequest(response);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await service.GetAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TipoDocumentoRequestDto expedienteRequestDto)
        {

            var response = await service.AddAsync(expedienteRequestDto);
            return response.Success ? Ok(response) : BadRequest(Response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, TipoDocumentoRequestDto expedienteRequestDto)
        {
            var response = await service.UpdateAsync(id, expedienteRequestDto);
            return response.Success ? Ok(response) : BadRequest(response);

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await service.DeleteAsync(id);
            return response.Success ? Ok(response) : BadRequest(Response);

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
