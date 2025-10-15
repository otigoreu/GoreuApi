using Goreu.Dto.Request.Pide;

namespace Goreu.API.Controllers.Pide.Integraciones
{
    [ApiController]
    [Route("api/reniec")]
    public class ReniecController : ControllerBase
    {

        private readonly IReniecService _reniecService;

        public ReniecController(IReniecService reniecService)
        {
            _reniecService = reniecService;
        }

        /// <summary>
        /// Consulta los datos personales de un ciudadano utilizando el servicio de RENIEC a través de la PIDE.
        /// </summary>
        /// <param name="request">
        /// Objeto de tipo <see cref="GetReniecRequest"/> que contiene los parámetros necesarios para realizar la consulta:
        /// - <c>nuDniConsulta</c>: Número de DNI de la persona cuyos datos se desea obtener.
        /// - <c>nuDniUsuario</c>: Número de DNI del usuario autorizado (registrado ante RENIEC).
        /// - <c>nuRucUsuario</c>: Número de RUC de la entidad que realiza la consulta.
        /// </param>
        /// <returns>
        /// Retorna un resultado HTTP con la información del ciudadano consultado o el detalle del error:
        /// - <c>200 OK</c>: Si la consulta fue exitosa. Devuelve un objeto de tipo <see cref="ReniecResponseModel"/> con los datos del ciudadano:
        ///   - <c>prenombres</c>: Nombres del ciudadano.
        ///   - <c>apPrimer</c>: Primer apellido.
        ///   - <c>apSegundo</c>: Segundo apellido.
        ///   - <c>direccion</c>: Dirección actual.
        ///   - <c>estadoCivil</c>: Estado civil.
        ///   - <c>ubigeo</c>: Ubicación geográfica (Departamento / Provincia / Distrito).
        ///   - <c>foto</c>: Imagen del ciudadano en base64.
        /// - <c>400 Bad Request</c>: Si las credenciales del usuario no existen o los datos de entrada son inválidos.
        /// - <c>404 Not Found</c>: Si el DNI consultado no fue encontrado o RENIEC devolvió un código distinto de “0000”.
        /// - <c>500 Internal Server Error</c>: Si ocurre un error inesperado durante la comunicación o procesamiento con RENIEC.
        /// </returns>
        /// <remarks>
        /// Este endpoint realiza las siguientes operaciones de forma automática:
        /// 1. Consulta los datos en RENIEC utilizando las credenciales registradas en la base de datos.
        /// 2. Si RENIEC devuelve el código <c>1002</c> (contraseña caducada), actualiza la contraseña automáticamente, 
        ///    la guarda en base de datos y reintenta la consulta.
        /// 3. Devuelve los datos completos del ciudadano si la operación fue exitosa.
        /// </remarks>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetReniecRequest request)
        {
            try
            {
                var result = await _reniecService.ConsultarPersonaAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
