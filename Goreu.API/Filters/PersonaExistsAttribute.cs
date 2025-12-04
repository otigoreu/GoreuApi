namespace Goreu.API.Filters
{
    public class PersonaExistsAttribute : TypeFilterAttribute
    {
        public PersonaExistsAttribute() : base(typeof(PersonaExistsFilterImpl)) { }

        private class PersonaExistsFilterImpl : IAsyncActionFilter
        {
            private readonly IPersonaService _personaService;

            public PersonaExistsFilterImpl(IPersonaService personaService)
            {
                _personaService = personaService;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                // Obtener el DTO desde los argumentos
                if (!context.ActionArguments.TryGetValue("personRequestDto", out var dtoObj) ||
                    dtoObj is not PersonaRequestDto dto)
                {
                    context.Result = new BadRequestObjectResult(new BaseResponse
                    {
                        Success = false,
                        ErrorMessage = "El cuerpo de la solicitud es inválido o falta."
                    });
                    return;
                }

                // Validar nroDoc dentro del DTO
                if (string.IsNullOrWhiteSpace(dto.nroDoc))
                {
                    context.Result = new BadRequestObjectResult(new BaseResponse
                    {
                        Success = false,
                        ErrorMessage = "El número de documento (nroDoc) es obligatorio."
                    });
                    return;
                }

                // Consulta la existencia
                var persona = await _personaService.GetByNumDocumentoAsync(dto.nroDoc);

                if (persona.Success)
                {
                    context.Result = new ConflictObjectResult(new BaseResponse
                    {
                        Success = false,
                        ErrorMessage = $"Ya existe una persona registrada con el número de documento: {dto.nroDoc}."
                    });
                    return;
                }

                await next();
            }
        }
    }
}
