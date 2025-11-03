using Goreu.Dto.Response;

namespace Goreu.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {
        private readonly IUserService usuarioService;
        private readonly IConfiguration configuration;

        public UsersController(IUserService usuarioService, IConfiguration configuration)
        {
            this.usuarioService = usuarioService;
            this.configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var response = await usuarioService.RegisterAsync(request);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, UsuarioRequestDto request)
        {
            var response = await usuarioService.updateAsync(id, request);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var response = await usuarioService.LoginAsync(request);
            return response.Success ? Ok(response) : Unauthorized(response);
        }

        [HttpPost("RequestTokenToResetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> RequestTokenToResetPassword(ResetPasswordRequestDto request)
        {
            // Determinar URL según entorno
            var frontResetPassword = configuration["FrontResetPassword:url"];

            var response = await usuarioService.RequestTokenToResetPasswordAsync(frontResetPassword, request);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost("ResetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] NewPasswordRequestDto request)
        {
            var response = await usuarioService.ResetPasswordAsync(request);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePasswordUserName([FromBody] ChangePasswordRequestDto request)
        {
            //Obtener eil del token actual
            var userName = HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Name).Value;

            var response = await usuarioService.ChangePasswordAsyncUserName(userName, request);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        //-------------------------------------------------------------------------------------------
        //trae los usuarios con ese rol
        
        [HttpGet("GetUsersByRole")]
        public async Task<IActionResult> GetUsersByRole([FromQuery] string? role = "")
        {

            var response = await usuarioService.GetUsersByRole(role);
            return response.Success ? Ok(response) : BadRequest(response);

        }

        [HttpGet("GetUserbyEmail")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var response = await usuarioService.GetUserByEmail(email);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("persona/{idPersona:int}")]
        public async Task<IActionResult> GetByIdPersonaAsync(int idPersona)
        {
            var result = await usuarioService.GetByIdPersonaAsync(idPersona);

            if (!result.Success)
            {
                // 📦 Si no se encontró el usuario
                if (result.ErrorMessage?.Contains("no se encontró", StringComparison.OrdinalIgnoreCase) == true)
                    return NotFound(new BaseResponseGeneric<object>
                    {
                        Success = false,
                        ErrorMessage = result.ErrorMessage
                    });

                // ⚠️ Si hubo un error inesperado
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponseGeneric<object>
                {
                    Success = false,
                    ErrorMessage = result.ErrorMessage
                });
            }

            // ✅ Éxito
            return Ok(new BaseResponseGeneric<UsuarioResponseDto>
            {
                Success = true,
                Data = result.Data
            });
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByIdAsync(string userId)
        {
            var result = await usuarioService.GetByIdAsync(userId);

            if (!result.Success)
            {
                // 📦 Si no se encontró el usuario
                if (result.ErrorMessage?.Contains("no se encontró", StringComparison.OrdinalIgnoreCase) == true)
                    return NotFound(new BaseResponseGeneric<object>
                    {
                        Success = false,
                        ErrorMessage = result.ErrorMessage
                    });

                // ⚠️ Si hubo un error inesperado
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponseGeneric<object>
                {
                    Success = false,
                    ErrorMessage = result.ErrorMessage
                });
            }

            // ✅ Éxito
            return Ok(new BaseResponseGeneric<UsuarioResponseDto>
            {
                Success = true,
                Data = result.Data
            });
        }
        //-------------------------------------------------------------------------------------------

        [HttpPost("roles/grant/{userId}")]
        public async Task<IActionResult> GrantRole(string userId, string RoleName)
        {
            var response = await usuarioService.GrantUserRole(userId, RoleName);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost("roles/grantByEmail/{email}")]
        
        public async Task<IActionResult> GrantRolesByEmail(string email, string roleName)
        {
            var response = await usuarioService.GrantUserRoleByEmail(email, roleName);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        
        [HttpPost("roles/revoke/{userId}")]
        public async Task<IActionResult> RevokeRoles(string userId)
        {
            var response = await usuarioService.RevokeUserRoles(userId);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        
        [HttpPost("role/revoke/{userId}")]
        public async Task<IActionResult> RevokeRole(string userId, string roleName)
        {
            var response = await usuarioService.RevokeUserRole(userId, roleName);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet]
        [AllowAnonymous] // -----------------------------------------------------------------------------------------------------------> BORRAR
        public async Task<IActionResult> Get([FromQuery] int idEntidad, [FromQuery] int idAplicacion, [FromQuery] string? rolId, [FromQuery] string? search, [FromQuery] PaginationDto? pagination)
        {
            var response = await usuarioService.GetAsync(idEntidad, idAplicacion, rolId, search, pagination);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPatch("{id}/finalize")]
        public async Task<IActionResult> Finalize(string id)
        {
            var response = await usuarioService.FinalizeAsync(id);

            if (!response.Success)
                return NotFound(response); // o BadRequest según el motivo

            return Ok(response);
        }

        [HttpPatch("{id}/initialize")]
        public async Task<IActionResult> Initialize(string id)
        {
            var response = await usuarioService.InitializeAsync(id);

            if (!response.Success)
                return NotFound(response); // o BadRequest según el motivo

            return Ok(response);
        }

        [HttpPatch("{id}/force-password")]
        public async Task<IActionResult> ForcePasswordChange([FromRoute] string id)
        {
            var response = await usuarioService.ForcePasswordChangeAsync(id);

            if (!response.Success)
            {
                // Si el servicio detecta que el usuario no existe
                if (response.ErrorMessage?.Contains("No se encontró") == true)
                    return NotFound(response);

                // Otros errores → BadRequest
                return BadRequest(response);
            }

            return Ok(response);
        }

    }
}
