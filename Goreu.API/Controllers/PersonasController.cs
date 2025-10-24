using Goreu.Dto.Response;
using Goreu.Entities.Info;

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

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] string? search,
            [FromQuery] PaginationDto? pagination = null)
        {
            var result = await service.GetAsync(search, pagination);

            return result.Success ? Ok(result) : StatusCode(500, result.ErrorMessage);
        }

        //[HttpGet("email")]
        //public async Task<IActionResult> Get(string? email)
        //{
        //    var response = await service.GetAsyncBYEmail(email);
        //    return response.Success ? Ok(response) : BadRequest(response);
        //}

        /// <summary>
        /// Consulta los datos personales de una persona registrada en el sistema,
        /// utilizando su número de documento como identificador único.
        /// </summary>
        /// <param name="numDocumento">
        /// Número de documento de la persona a consultar. Puede ser un DNI, RUC u otro tipo de documento según la configuración del sistema.
        /// </param>
        /// <returns>
        /// Retorna un resultado HTTP con la información de la persona consultada o el detalle del error:
        /// - <c>200 OK</c>: Si la consulta fue exitosa. Devuelve un objeto de tipo <see cref="PersonaInfo"/> con los datos de la persona:
        ///   - <c>nroDoc</c>: Número de documento.
        ///   - <c>nombres</c>: Nombres de la persona.
        ///   - <c>apellidoPat</c>: Apellido paterno.
        ///   - <c>apellidoMat</c>: Apellido materno.
        ///   - <c>correo</c>: Dirección de correo electrónico (si aplica).
        ///   - <c>telefono</c>: Número de teléfono (si aplica).
        /// - <c>400 Bad Request</c>: Si no se proporcionó un número de documento válido o hubo un error de validación.
        /// - <c>404 Not Found</c>: Si no se encontró ninguna persona con el número de documento especificado.
        /// - <c>500 Internal Server Error</c>: Si ocurre un error inesperado durante la ejecución o conexión con la base de datos.
        /// </returns>
        /// <remarks>
        /// Este endpoint realiza las siguientes operaciones de forma automática:
        /// 1. Busca en la base de datos una persona cuyo número de documento coincida con el valor recibido.
        /// 2. Si se encuentra un registro, lo transforma en un objeto de tipo <see cref="PersonaInfo"/> antes de devolverlo.
        /// 3. Si no se encuentra resultado, devuelve un mensaje indicando que no existen registros para el documento proporcionado.
        /// </remarks>
        [HttpGet("numDocumento")]
        public async Task<IActionResult> GetByNumDocumento([FromQuery] string numDocumento)
        {
            if (string.IsNullOrWhiteSpace(numDocumento))
                return BadRequest(new { ErrorMessage = "El número de documento es requerido." });

            var response = await service.GetByNumDocumentoAsync(numDocumento);

            if (!response.Success && response.ErrorMessage.Contains("No se encontró"))
                return NotFound(response);

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
