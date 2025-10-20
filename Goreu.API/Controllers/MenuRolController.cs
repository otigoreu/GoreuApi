namespace Goreu.API.Controllers
{
    [ApiController]
    [Route("api/menuroles")]
    public class MenuRolController :ControllerBase
    {
        private readonly IMenuRolService service;

        public MenuRolController(IMenuRolService service) {
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post(MenuRolRequestDto menuRolRequestDto)
        {
            var response = await service.AddAsync(menuRolRequestDto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await service.DeleteAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, MenuRolRequestDto menuRolRequestDto)
        {
            var response = await service.UpdateAsync(id, menuRolRequestDto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await service.GetAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await service.GetAsync();

            return result.Success ? Ok(result) : StatusCode(500, result.ErrorMessage);
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
        [HttpGet("rolmenusestado")]
        public async Task<IActionResult> GetUsuariosPaginadas(int idEntidad, int idAplicacion, string idRol)
         
        {
            var result = await service.GetMenusConEstadoPorRolAsync(idEntidad, idAplicacion, idRol);

            return result.Success ? Ok(result) : StatusCode(500, result.ErrorMessage);
        }

        [HttpGet("byIdRolAndIdMenu")]
        public async Task<IActionResult> Get( string idRol,int idMenu)
        {
            var result = await service.GetAsync(idRol, idMenu);

            return Ok(result);
            //return result.Success ? Ok(result) : StatusCode(500, result.ErrorMessage);
        }

    }
}
