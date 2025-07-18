using Goreu.Dto.Request.Pide.Credenciales;
using Goreu.Services.Interface.Pide;
using Microsoft.AspNetCore.Mvc;

namespace Goreu.API.Controllers.Pide.Credenciales
{
    [ApiController]
    [Route("api/credencialesreniec")]
    public class CredencialesReniecController : ControllerBase
    {
        private readonly ICredencialReniecService service;

        public CredencialesReniecController(ICredencialReniecService service)
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
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        public async Task<IActionResult> Post(AddCredencialReniecRequestDto sedeRequestDto)
        {
            var response = await service.AddAsync(sedeRequestDto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, AddCredencialReniecRequestDto sedeRequestDto)
        {
            var response = await service.UpdateAsync(id, sedeRequestDto);
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
