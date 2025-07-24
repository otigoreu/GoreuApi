namespace Goreu.API.Controllers.Pide.Integraciones
{
    [ApiController]
    [Route("api/reniec")]
    public class ReniecController : ControllerBase
    {
        private readonly ICredencialReniecService _credencialReniecService;
        private readonly HttpClient _httpClient;

        public ReniecController(IHttpClientFactory httpClientFactory, ICredencialReniecService credencialReniecService)
        {
            _httpClient = httpClientFactory.CreateClient();
            this._credencialReniecService = credencialReniecService;
        }

        [HttpGet]
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get([FromQuery] GetReniecRequest request)
        {
            // Paso 1: Obtener las credenciales del usuario
            var credencialResult = await _credencialReniecService.GetByNumdocAsync(request.nuDniUsuario);

            if (!credencialResult.Success || credencialResult.Data == null)
                return BadRequest("Credencial RENIEC no encontrada.");

            try
            {
                // Paso 2: Preparar el payload para la API RENIEC via PIDE
                using var client = new HttpClient();
                var pideUrl = "https://ws2.pide.gob.pe/Rest/RENIEC/Consultar";

                var payload = new
                {
                    PIDE = new
                    {
                        nuDniConsulta = request.nuDniConsulta,
                        nuDniUsuario = request.nuDniUsuario,
                        nuRucUsuario = credencialResult.Data.nuRucUsuario,
                        password = credencialResult.Data.password
                    }
                };

                var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(pideUrl, content);

                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, "Error al consultar RENIEC.");

                var xmlContent = await response.Content.ReadAsStringAsync();
                var xdoc = XDocument.Parse(xmlContent);

                // Paso 3: Leer respuesta SOAP con namespaces
                XNamespace soap = "http://schemas.xmlsoap.org/soap/envelope/";
                XNamespace reniec = "http://ws.reniec.gob.pe/";

                var resultElement = xdoc
                    .Element(soap + "Envelope")?
                    .Element(soap + "Body")?
                    .Element(reniec + "consultarResponse")?
                    .Element("return");

                if (resultElement == null)
                    return StatusCode(500, "No se pudo procesar la respuesta de RENIEC.");

                // Paso 4: Validar si hubo error
                var coResultado = resultElement.Element("coResultado")?.Value;
                var deResultado = resultElement.Element("deResultado")?.Value;

                if (!string.IsNullOrEmpty(coResultado) && coResultado != "0000")
                {
                    return NotFound(new
                    {
                        Codigo = coResultado,
                        Mensaje = deResultado
                    });
                }

                // Paso 5: Extraer los datos personales desde <datosPersona>
                var datosPersonaElement = resultElement.Element("datosPersona");

                if (datosPersonaElement == null)
                    return StatusCode(500, "No se encontraron los datos personales en la respuesta.");

                var resultado = new
                {
                    nombre = datosPersonaElement.Element("prenombres")?.Value,
                    apepaterno = datosPersonaElement.Element("apPrimer")?.Value,
                    apematerno = datosPersonaElement.Element("apSegundo")?.Value,
                    direccion = datosPersonaElement.Element("direccion")?.Value,
                    estadoCivil = datosPersonaElement.Element("estadoCivil")?.Value,
                    restriccion = datosPersonaElement.Element("restriccion")?.Value,
                    ubigeo = datosPersonaElement.Element("ubigeo")?.Value,
                    foto = datosPersonaElement.Element("foto")?.Value
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                // Aquí podrías registrar el error con un logger si lo deseas
                return StatusCode(500, "Ocurrió un error inesperado al consultar RENIEC.");
            }
        }

    }
}
