
using Goreu.Entities;

namespace Goreu.Services.Implementation
{
    public class MenuRolService : IMenuRolService
    {

        private readonly IMenuRolRepository repository;
        private readonly ILogger<MenuRolService> logger;
        private readonly IMapper mapper;
        private readonly IMenuRepository menuRepository;
        private readonly IRolRepository rolRepository;
        private IMenuService menuService;
        public MenuRolService(
            IMenuRolRepository repository,
            ILogger<MenuRolService> logger,
            IMapper mapper,
            IMenuRepository menuRepository,
            IRolRepository rolRepository,
            IMenuService menuService
            ) { 

            this.repository = repository;
            this.logger = logger;
            this.mapper=mapper;
            this.menuRepository = menuRepository;
            this.rolRepository = rolRepository;
            this.menuService = menuService;
        
        }
        public async  Task<BaseResponseGeneric<int>> AddAsync(MenuRolRequestDto request)
        {
            var response = new BaseResponseGeneric<int>();

            try
            {
                response.Data = await repository.AddAsync(mapper.Map<MenuRol>(request));
                response.Success = true;

            }
            catch (Exception ex)
            {

                response.ErrorMessage="Ocurrio un error al guardar los datos";
                logger.LogError(ex,"{ErrorMensage} {Message}", response.ErrorMessage, ex.Message);
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

                response.ErrorMessage = "Ocurrio un error al Eliminar los datos";
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
                response.ErrorMessage = "Ocurrio un error al finalizar los datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponseGeneric<MenuRolResponseDto>> GetAsync(int id)
        {
            var response = new BaseResponseGeneric<MenuRolResponseDto>();
            try
            {
                var data = await repository.GetAsync(id);
                response.Data = mapper.Map<MenuRolResponseDto>(data);
                Console.WriteLine("Persona", response.Data.IdMenu);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener los datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<MenuRolResponseDto>>> GetAsync()
        {
            var response = new BaseResponseGeneric<ICollection<MenuRolResponseDto>>();
            try
            {
                var data = await repository.GetAsync();

                response.Data = mapper.Map<ICollection<MenuRolResponseDto>>(data);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener los datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponseGeneric<MenuRolInfo>> GetAsync(string idRol, int idMenu)
        {
            var response = new BaseResponseGeneric<MenuRolInfo>();

            try
            {
                var entity =await repository.GetAsync(idRol,idMenu);
                if (entity is null) {
                    response.ErrorMessage = $"El Rol con ID {idRol} y el Menucon ID {idMenu} no existe.";
                    return response;
                }
                response.Data = mapper.Map<MenuRolInfo>(entity);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al obtener la entidad.";
                logger.LogError(ex, "{ErrorMessage} {Exception}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<MenuRolInfo>>> GetMenusConEstadoPorRolAsync(int idEntidad, int idAplicacion, string IdRol)
        {
            var response = new BaseResponseGeneric<ICollection<MenuRolInfo>>();

            try
            {
                //PAso 1.- Obtener todos los Menus
                var todosLosMenus = await menuService.GetAllByEntidadAndAplicacion(idEntidad,idAplicacion);

                //Paso 2.- todos las relaciones de MenuRol por el id
                var menuRol = await repository.GetAsync(mr => mr.IdRol.Equals(IdRol));

                //Paso 3.- hacer join entre todos los MenuRol y los menus

                var resultado = todosLosMenus.Data.Select(mero => {

                    var asociada = menuRol.FirstOrDefault(mr => mr.IdMenu==mero.Id);

                    return new MenuRolInfo { 
                    
                        Id=asociada?.Id??0,
                        Operacion=asociada?.Operacion??false,
                        Consulta=asociada?.Consulta??false,
                        IdRol=asociada?.IdRol??"",
                        IconoMenu=mero?.Icono??"",
                        DescripcionMenu=mero?.Descripcion??"",
                        IdMenu=mero.Id,
                        Estado=asociada?.Estado??false,

                    };
                
                
                }).ToList();
                response.Data = resultado;
                response.Success = true;

            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al listar los usuarios habilitadas para la unidad organica.";
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

        public async Task<BaseResponse> UpdateAsync(int id, MenuRolRequestDto request)
        {
            var response = new BaseResponse();
            try
            {
                var data = await repository.GetAsync(id);
                if (data is null)
                {
                    response.ErrorMessage = $"la persona con id {id} no fue encontrado";
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
    }
}
