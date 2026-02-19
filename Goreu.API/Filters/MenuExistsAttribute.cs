namespace Goreu.API.Filters
{
    public class MenuExistsAttribute : TypeFilterAttribute
    {
        public MenuExistsAttribute()
            : base(typeof(MenuExistsFilterImpl)) { }

        private class MenuExistsFilterImpl : IAsyncActionFilter
        {
            private readonly IMenuService _menuService;

            public MenuExistsFilterImpl(IMenuService unidadOrganicaService)
            {
                _menuService = unidadOrganicaService;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                // Obtener el DTO desde los argumentos
                if (!context.ActionArguments.TryGetValue("request", out var dtoObj) ||
                    dtoObj is not MenuRequestDtoSingle dto)
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

                var existeResponse = await _menuService
                    .ValidarDescripcionAsync(dto.Descripcion, dto.IdAplicacion);

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
                        ErrorMessage = $"Ya existe un menu registrada con esa descripción: {dto.Descripcion}."
                    });
                    return;
                }

                await next();
            }
        }
    }
}

