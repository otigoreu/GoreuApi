using AutoMapper;
using Goreu.Dto.Request;
using Goreu.Dto.Response;
using Goreu.Entities;
using Goreu.Entities.Info;
using Goreu.Repositories.Interface;
using Goreu.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Goreu.Services.Implementation
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository repository;
        private readonly ILogger<MenuService> logger;
        private readonly IMapper mapper;
        private readonly UserManager<Usuario> usuario;
        private readonly RoleManager<IdentityRole> roleManager;

        public MenuService(IMenuRepository repository,
            ILogger<MenuService> logger,
            IMapper mapper,
            UserManager<Usuario> usuario,
            RoleManager<IdentityRole> roleManager)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
            this.usuario = usuario;
            this.roleManager = roleManager;
        }
        public async Task<BaseResponseGeneric<int>> AddAsync(MenuRequestDto request)
        {
            var response = new BaseResponseGeneric<int>();
            try
            {
                var roles = new List<MenuRol>();

                var menuDb = new Menu
                {
                    Descripcion = request.Descripcion,
                    Icono = request.Icono,
                    Ruta = request.Ruta,
                    IdAplicacion = request.IdAplicacion,
                    IdMenuPadre = request.IdMenuPadre == 0 ? null : request.IdMenuPadre
                };
                response.Data = await repository.AddAsync(menuDb);// aca ya lo añade a la base de datos?

                foreach (var item in request.IdRoles)
                {
                    roles.Add(new MenuRol { IdMenu = menuDb.Id, IdRol = item });// esto agrega a la base de datos?
                }
                menuDb.MenuRoles = roles;
                await repository.UpdateAsync();

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al añadir la información";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponseGeneric<int>> AddAsyncSingle(MenuRequestDtoSingle request)
        {

            var response = new BaseResponseGeneric<int>();
            try
            {
                response.Data = await repository.AddAsync(mapper.Map<Menu>(request));
                response.Success = true;


            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Ocurrio un error al guardar los datos";
                logger.LogError(ex, "{ErrorMessage}{Message}", response.ErrorMessage, ex.Message);
            }

            return response;

        }

        public async Task<BaseResponseGeneric<ICollection<MenuResponseDto>>> GetByAplicationAsync(int idAplication, string userName)
        {
            var response = new BaseResponseGeneric<ICollection<MenuResponseDto>>();
            try
            {
                // var persona = await usuario.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
                var persona = await usuario.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
                //este metodo .. es normal se puede usar siempre?
                IList<string> roles = new List<string>();
                if (persona is not null)
                {
                    roles = await usuario.GetRolesAsync(persona);
                }
                else
                {
                    response.ErrorMessage = "No existe el usuario.";
                    return response;
                }
                //var menusDb = await repository.GetByIdAplicationAsync(idAplication);
                var roleIds = new List<string>();
                foreach (var role in roles)
                {
                    var roleEntity = await roleManager.FindByNameAsync(role);// con que se comprara
                    if (roleEntity != null)
                    {
                        roleIds.Add(roleEntity.Id);
                    }
                }

                // Filtrar menús que coincidan con la aplicación y roles del usuario
                var menusDb = await repository.GetMenusByApplicationAndRolesAsync(idAplication, roleIds);

                if (menusDb.Count > 0)
                {
                    response.Data = mapper.Map<ICollection<MenuResponseDto>>(menusDb);
                    response.Success = true;
                }
                else
                {
                    response.ErrorMessage = $"No hay menús asignados a la aplicación con id {idAplication}.";
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al añadir la información";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }
        public async Task<BaseResponseGeneric<ICollection<MenuInfo>>> GetAsync(string? Descripcion)
        {
            var response = new BaseResponseGeneric<ICollection<MenuInfo>>();
            try
            {

                response.Data = await repository.GetAsync(Descripcion);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener los datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<MenuInfoRol>>> GetAsyncWithRole(string? Descripcion)
        {
            var response = new BaseResponseGeneric<ICollection<MenuInfoRol>>();
            try
            {

                response.Data = await repository.GetAsyncWithRole(Descripcion);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener los datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> DeleteAsync(int id)
        {
            var response = new BaseResponse();
            try
            {
                await repository.DeleteAsync(id);
                response.Success = true;

            }
            catch (Exception ex)
            {

                response.ErrorMessage = "Ocurrio un error al deshabilitar los datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }
        public async Task<BaseResponse> InitializedAsync(int id)
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
        public async Task<BaseResponse> FinalizedAsync(int id)
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

        public async Task<BaseResponse> UpdateAsync(int id, MenuRequestDto request)
        {
            var response = new BaseResponse();
            try
            {
                var data = await repository.GetAsync(id);
                if (data is null)
                {
                    response.ErrorMessage = $"El Menu con id {id} no fue encontrado";
                }

                mapper.Map(request, data);
                await repository.UpdateAsync();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al actualizar  los datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> UpdateAsyncSingle(int id, MenuRequestDtoSingle request)
        {
            var response = new BaseResponse();
            try
            {
                var data = await repository.GetAsync(id);
                if (data is null)
                {
                    response.ErrorMessage = $"El Menu con id {id} no fue encontrado";
                }

                mapper.Map(request, data);
                await repository.UpdateAsync();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al actualizar  los datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<MenuInfo>>> GetByAplicationAsyncSingle(int idAplication)
        {
            var response = new BaseResponseGeneric<ICollection<MenuInfo>>();
            try
            {

                response.Data = mapper.Map<ICollection<MenuInfo>>(await repository.GetByIdAplicationAsync(idAplication));
                response.Success = true;

            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al obtener la información";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }
    }
}
