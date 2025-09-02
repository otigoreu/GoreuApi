using AutoMapper;
using Goreu.Dto.Request;
using Goreu.Dto.Response;
using Goreu.DtoResponse;
using Goreu.Entities;
using Goreu.Persistence;
using Goreu.Repositories.Interface;
using Goreu.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Goreu.Services.Implementation
{
    public class RolService : IRolService
    {
        private readonly RoleManager<IdentityRole> rolManager;
        private readonly ILogger<RolService> logger;
        private readonly IConfiguration configuration;
        private readonly SignInManager<Rol> singInManager;
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;
        private readonly IRolRepository repository;

        public RolService(
            RoleManager<IdentityRole> rolManager,
            ILogger<RolService> logger,
            IConfiguration configuration,
            IMapper mapper,
            ApplicationDbContext context,
            IRolRepository repository
            )
        {

            this.rolManager = rolManager;
            this.logger = logger;
            this.configuration = configuration;
            this.mapper = mapper;
            this.context = context;
            this.repository = repository;
        }

        //FUNCIONA
        public async Task<BaseResponseGeneric<string>> AddSync(RolRequestDto request)
        {
            var response = new BaseResponseGeneric<string>();
            try
            {
                if (await rolManager.RoleExistsAsync(request.Name))
                {
                    response.ErrorMessage = "Rol ya existe";
                    return response;
                }
                response.Data = await repository.AddAsync(mapper.Map<Rol>(request));
                response.Success = true;

                return response;

            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Ocurrio un error al registrar el rol";
                logger.LogError(ex, "{ErrorMessage}{Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }
        //FUNCIONA
        public async Task<BaseResponse> DeleteAsync(string id)
        {
            var response = new BaseResponse();
            try
            {
                await repository.DeleteAsync(id);
                response.Success = true;
            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Ocurrio un error al Eliminar los datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> FinalizedAsync(string id)
        {
            var response = new BaseResponse();
            try
            {
                await repository.FinalizedAsync(id);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al finalizar Datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        //FUNCIONA
        public async Task<BaseResponseGeneric<ICollection<RolResponseDto>>> GetAsync()
        {
            var response = new BaseResponseGeneric<ICollection<RolResponseDto>>();
            try
            {
                var data = await repository.GetAllAsync();
                response.Data = mapper.Map<ICollection<RolResponseDto>>(data);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener los datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        //FUNCIONA
        public async Task<BaseResponseGeneric<RolResponseDto>> GetAsync(string id)
        {
            var response = new BaseResponseGeneric<RolResponseDto>();
            try
            {
                var data = await repository.GetAsync(id);
                response.Data = mapper.Map<RolResponseDto>(data);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener los datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<RolResponseSingleDto>>> GetAsyncPerUser(string idUser)
        {
            var response = new BaseResponseGeneric<ICollection<RolResponseSingleDto>>();
            try
            {
               var data=await repository.GetAsyncPerUser(idUser);
                response.Data = mapper.Map<ICollection<RolResponseSingleDto>>(data);
                response.Success = true;

            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Ocurrio un error al obtener los datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> InitializedAsync(string id)
        {
            var response = new BaseResponse();
            try
            {
                await repository.InitializedAsync(id);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al Inicializar Datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        //FUNCIONA
        public async Task<BaseResponse> UpdateAsync(string id, RolRequestDto request)
        {
            var response = new BaseResponse();

            try
            {
                var data = await rolManager.FindByIdAsync(id);
                if (data is null)
                {
                    response.ErrorMessage = $"El Rol con id {id} no fue encontrado";
                }
                await rolManager.UpdateAsync(mapper.Map(request, data));
                response.Success = true;

            }
            catch (Exception ex)
            {

                response.ErrorMessage = "";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<RolPaginationResponseDto>>> GetAsync(int idEntidad, int idAplicacion, string? search, PaginationDto? pagination)
        {
            var response = new BaseResponseGeneric<ICollection<RolPaginationResponseDto>>();

            try
            {
                var data = await repository.GetAsync(idEntidad, idAplicacion, search, pagination);

                response.Data = mapper.Map<ICollection<RolPaginationResponseDto>>(data);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al listar los roles para la entidad y aplicacion.";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }
    }
}
