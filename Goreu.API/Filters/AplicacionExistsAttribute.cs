namespace Goreu.API.Filters
{
    public class AplicacionExistsAttribute : TypeFilterAttribute
    {
        public AplicacionExistsAttribute() : base(typeof(AplicacionExistsFilterImpl)) { }

        private class AplicacionExistsFilterImpl : IAsyncActionFilter
        {
            private readonly IAplicacionService _aplicacionService;

            public AplicacionExistsFilterImpl(IAplicacionService aplicacionService)
            {
                _aplicacionService = aplicacionService;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                // Obtener el DTO desde los argumentos
                if (!context.ActionArguments.TryGetValue("dto", out var dtoObj) ||
                    dtoObj is not AplicacionRequestDto dto)
                {
                    context.Result = new BadRequestObjectResult(new BaseResponse
                    {
                        Success = false,
                        ErrorMessage = "El cuerpo de la solicitud es inválido o falta."
                    });
                    return;
                }

                // Consulta la existencia
                var result = await _aplicacionService.ValidarDescripcionAsync(dto.Descripcion);

                if (result.Success)
                {
                    if (result.Data)
                    {
                        context.Result = new ConflictObjectResult(new BaseResponse
                        {
                            Success = false,
                            ErrorMessage = $"Ya existe una aplicación registrada con esa descripción: {dto.Descripcion}."
                        });
                        return;
                    }
                }

                await next();
            }
        }
    }
}
