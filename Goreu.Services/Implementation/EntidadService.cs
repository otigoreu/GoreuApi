using AutoMapper;
using Goreu.Dto;
using Goreu.Dto.Request;
using Goreu.Dto.Response;
using Goreu.Entities;
using Goreu.Repositories.Interface;
using Goreu.Services.Interface;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Goreu.Services.Implementation
{
    public class EntidadService : ServiceBase<Entidad, EntidadRequestDto, EntidadResponseDto>, IEntidadService
    {
        private readonly IEntidadRepository repository;
        private readonly IUserService userService;
        private readonly IRolService rolService;
        private readonly IEntidadAplicacionService entidadaplicacionService;

        private readonly IUsuarioUnidadOrganicaService user_uoService;


        public EntidadService(IEntidadRepository repository, IUserService userService, IRolService rolService, IEntidadAplicacionService entidadaplicacionService, IUsuarioUnidadOrganicaService user_uoService, ILogger<EntidadService> logger, IMapper mapper) : base(repository, logger, mapper)
        {
            this.repository = repository; // ✅ Asignación correcta
            this.userService = userService;
            this.rolService = rolService;
            this.entidadaplicacionService = entidadaplicacionService;
            this.user_uoService = user_uoService;
        }

        public async Task<BaseResponseGeneric<ICollection<EntidadResponseDto>>> GetAsync(string userId, string rolId, string? descripcion, PaginationDto? pagination)
        {
            var response = new BaseResponseGeneric<ICollection<EntidadResponseDto>>();
            try
            {
                descripcion ??= string.Empty; // Evita nulls
                ICollection<Entidad> data;

                var user = await userService.GetUserByIdAsync(userId);
                if (user?.Data == null)
                {
                    response.ErrorMessage = "Usuario no encontrado.";
                    return response;
                }

                var rol = await rolService.GetAsync(rolId);
                if (rol?.Data == null)
                {
                    response.ErrorMessage = "Rol no encontrado.";
                    return response;
                }

                Expression<Func<Entidad, bool>> predicate;
                if (rol.Data.Nivel == '1')
                {  // -->> Sistema
                    predicate = s => s.Descripcion.Contains(descripcion);
                }
                else              
                {
                    var entidadaplicacion = await entidadaplicacionService.GetAsync(rol.Data.idEntidadAplicacion);

                    predicate = s => s.Id == entidadaplicacion.Data.IdEntidad &&
                                 s.Descripcion.Contains(descripcion);
                }

                data = await repository.GetAsync(
                    predicate: predicate,
                    orderBy: x => x.Descripcion,
                    pagination);

                response.Data = mapper.Map<ICollection<EntidadResponseDto>>(data);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al filtrar las unidades orgánicas por descripción.";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        public async Task<BaseResponseGeneric<EntidadResponseDto>> GetAsyncPerUser(string idUser)
        {
            var response = new BaseResponseGeneric<EntidadResponseDto>();

            try
            {
                var data = await repository.GetAsyncPerUser(idUser);
                response.Data = mapper.Map<EntidadResponseDto>(data);
                response.Success = true;

            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Ocurrio un error al obtener los datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }
    }
}
