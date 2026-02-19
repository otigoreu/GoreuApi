namespace Goreu.API.Filters
{
    public class TipoDocumentoExistsAttribute : TypeFilterAttribute
    {
        public TipoDocumentoExistsAttribute() : base(typeof(TipoDocumentoExistsFilterImpl)) { }

        private class TipoDocumentoExistsFilterImpl : IAsyncActionFilter
        {
            private readonly ITipoDocumentoService _tipodocumentoService;

            public TipoDocumentoExistsFilterImpl(ITipoDocumentoService tipodocumentoService)
            {
                _tipodocumentoService = tipodocumentoService;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                // Obtener el DTO desde los argumentos
                if (!context.ActionArguments.TryGetValue("expedienteRequestDto", out var dtoObj) ||
                    dtoObj is not TipoDocumentoRequestDto dto)
                {
                    context.Result = new BadRequestObjectResult(new BaseResponse
                    {
                        Success = false,
                        ErrorMessage = "El cuerpo de la solicitud es inválido o falta."
                    });
                    return;
                }

                if (string.IsNullOrWhiteSpace(dto.Descripcion))
                {
                    context.Result = new BadRequestObjectResult(new BaseResponse
                    {
                        Success = false,
                        ErrorMessage = "La descripción es obligatoria."
                    });
                    return;
                }

                var existeResponse = await _tipodocumentoService
                    .ValidarDescripcionAsync(dto.Descripcion);

                if (!existeResponse.Success)
                {
                    context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                    return;
                }

                if (existeResponse.Data) // 👈 AQUÍ está la validación real
                {
                    context.Result = new ConflictObjectResult(new BaseResponse
                    {
                        Success = false,
                        ErrorMessage = $"Ya existe un tipo de documento registrado con esa descripción: {dto.Descripcion}."
                    });
                    return;
                }

                await next();
            }
        }
    }
}

