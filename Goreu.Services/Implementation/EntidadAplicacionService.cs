using Goreu.Repositories.Implementation;

namespace Goreu.Services.Implementation
{
    public class EntidadAplicacionService : ServiceBase<EntidadAplicacion, EntidadAplicacionRequestDto, EntidadAplicacionResponseDto>, IEntidadAplicacionService
    {
        private readonly IEntidadAplicacionRepository repository;
        private readonly IRolRepository rolRepository;
        private readonly IEntidadAplicacionRepository entidadAplicacionRepository;

        private readonly IAplicacionService serviceAplicacion;
        private readonly IEntidadService serviceEntidad;

        public EntidadAplicacionService(IEntidadAplicacionRepository repository, IRolRepository rolRepository, IEntidadAplicacionRepository entidadAplicacionRepository, IAplicacionService serviceAplicacion, ILogger<EntidadAplicacionService> logger, IMapper mapper) : base(repository, logger, mapper)
        {
            this.repository = repository; // ✅ Asignación correcta
            this.rolRepository = rolRepository;
            this.entidadAplicacionRepository = entidadAplicacionRepository;
            this.serviceAplicacion = serviceAplicacion;
        }

        public async Task<BaseResponseGeneric<ICollection<AplicacionResponseDto>>> GetAplicacionesAsync(int idEntidad, string rolId)
        {
            var response = new BaseResponseGeneric<ICollection<AplicacionResponseDto>>();

            //try
            //{
            //    var rol = await rolRepository.GetAsync(rolId);
            //    var entidadAplicacion = await entidadAplicacionRepository.GetAsync(rol.IdEntidadAplicacion);

            //    ICollection<Aplicacion> data = rol.Nivel switch
            //    {
            //        '3' => await repository.GetAplicacionesAsync(
            //            predicate: z => z.IdAplicacion == entidadAplicacion.IdAplicacion && z.Aplicacion.Estado,
                        
            //            orderBy: z => z.Aplicacion.Descripcion,
            //            search: string.Empty,
            //            pagination: null
            //        ),
            //        '2' or '1' => await repository.GetAplicacionesAsync(
            //            predicate: z => z.IdEntidad == idEntidad && z.Aplicacion.Estado,
            //            orderBy: z => z.Aplicacion.Descripcion,
            //            search: string.Empty,
            //            pagination: null
            //        ),
            //        _ => new List<Aplicacion>()
            //    };

            //    response.Data = mapper.Map<ICollection<AplicacionResponseDto>>(data); 
            //    response.Success = true;
            //}
            //catch (Exception ex)
            //{
            //    response.ErrorMessage = "Error al listar las aplicaciones para la entidad.";
            //    logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            //}

            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<EntidadAplicacionResponseDto>>> GetAplicacionesConEstadoPorEntidadAsync(int idEntidad, string descripcion, PaginationDto pagination)
        {
            var response = new BaseResponseGeneric<ICollection<EntidadAplicacionResponseDto>>();

            try
            {
                // Paso 1: Obtener todas las aplicaciones
                var todasLasAplicaciones = await serviceAplicacion.GetAsync(descripcion, pagination); // List<Aplicacion>

                // Paso 2: Obtener las aplicaciones asociadas a la entidad
                var aplicacionesEntidad = await repository.GetAsync(ea => ea.IdEntidad == idEntidad);

                // Paso 3: Hacer join izquierdo entre todas las aplicaciones y las aplicaciones asociadas
                var resultado = todasLasAplicaciones
                    .Data
                    .Select(app =>
                    {
                        var asociada = aplicacionesEntidad.FirstOrDefault(ea => ea.IdAplicacion == app.Id);

                        return new EntidadAplicacionResponseDto
                        {
                            Id = asociada?.Id ?? 0, // Si no está asociada, Id = 0
                            IdEntidad = asociada?.IdEntidad ?? 0, // Si no está asociada, IdEntidad = 0
                            IdAplicacion = app.Id,
                            DescripcionAplicacion = app.Descripcion,
                            Estado = asociada?.Estado ?? false // Si no está asociada, se asume Estado = false
                        };
                    })
                    .OrderBy(x => x.DescripcionAplicacion)
                    .ToList();

                response.Data = resultado;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al listar las aplicaciones habilitadas para la entidad.";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        public async Task<BaseResponseGeneric<EntidadAplicacionResponseDto>> GetAsync(int idEntidad, int idAplicacion)
        {
            var response = new BaseResponseGeneric<EntidadAplicacionResponseDto>();
            try
            {
                var entity = await repository.GetAsync(idEntidad, idAplicacion);
                if (entity is null)
                {
                    response.ErrorMessage = $"La entidad con ID {idEntidad} y la aplicacion con ID {idAplicacion} no existe.";
                    return response;
                }

                response.Data = mapper.Map<EntidadAplicacionResponseDto>(entity);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al obtener la entidad.";
                logger.LogError(ex, "{ErrorMessage} {Exception}", response.ErrorMessage, ex.Message);
            }
            return response;
        }
    }
}
