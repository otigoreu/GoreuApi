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
        private readonly IUsuarioUnidadOrganicaService user_uoService;


        public EntidadService(IEntidadRepository repository, IUserService userService, IUsuarioUnidadOrganicaService user_uoService, ILogger<EntidadService> logger, IMapper mapper) : base(repository, logger, mapper)
        {
            this.repository = repository; // ✅ Asignación correcta
            this.userService = userService;
            this.user_uoService = user_uoService;
        }

        public async Task<BaseResponseGeneric<ICollection<EntidadResponseDto>>> GetAsync(string userId, string? descripcion, PaginationDto? pagination)
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

                Expression<Func<Entidad, bool>> predicate;

                if (user.Data.EsSuperUser)
                {
                    predicate = s => s.Descripcion.Contains(descripcion);
                }
                else
                {
                    var unidadesOrganicasUsuario = await user_uoService.GetUnidadOrganicasAsync(userId);

                    if (unidadesOrganicasUsuario?.Data == null || !unidadesOrganicasUsuario.Data.Any())
                    {
                        response.ErrorMessage = "El usuario no tiene unidades orgánicas asignadas.";
                        return response;
                    }

                    var primeraUnidadOrganica = unidadesOrganicasUsuario.Data.First();
                    predicate = s => s.Id == primeraUnidadOrganica.idEntidad &&
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
