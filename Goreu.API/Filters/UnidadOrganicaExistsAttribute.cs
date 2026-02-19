namespace Goreu.API.Filters
{
    public class UnidadOrganicaExistsAttribute : TypeFilterAttribute
    {
        public UnidadOrganicaExistsAttribute()
            : base(typeof(UnidadOrganicaExistsFilterImpl)) { }

        private class UnidadOrganicaExistsFilterImpl : IAsyncActionFilter
        {
            private readonly IUnidadOrganicaService _unidadOrganicaService;

            public UnidadOrganicaExistsFilterImpl(IUnidadOrganicaService unidadOrganicaService)
            {
                _unidadOrganicaService = unidadOrganicaService;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                // Obtener el DTO desde los argumentos
                if (!context.ActionArguments.TryGetValue("dto", out var dtoObj) ||
                    dtoObj is not UnidadOrganicaRequestDto dto)
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

                var existeResponse = await _unidadOrganicaService
                    .ValidarDescripcionAsync(dto.Descripcion, dto.IdEntidad);

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
                        ErrorMessage = $"Ya existe una unidad orgánica registrada con esa descripción: {dto.Descripcion}."
                    });
                    return;
                }

                await next();
            }
        }
    }
}

