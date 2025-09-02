namespace Goreu.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {
        private readonly IUserService service;

        public UsersController(IUserService service)
        {
            this.service = service;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var response = await service.RegisterAsync(request);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var response = await service.LoginAsync(request);
            return response.Success ? Ok(response) : Unauthorized(response);
        }

        [HttpPost("RequestTokenToResetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> RequestTokenToResetPassword(ResetPasswordRequestDto request)
        {
            var response = await service.RequestTokenToResetPasswordAsync(request);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost("ResetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] NewPasswordRequestDto request)
        {
            var response = await service.ResetPasswordAsync(request);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePasswordUserName([FromBody] ChangePasswordRequestDto request)
        {
            //Obtener eil del token actual
            var userName = HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Name).Value;
            var response = await service.ChangePasswordAsyncUserName(userName, request);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        //-------------------------------------------------------------------------------------------
        //trae los usuarios con ese rol
        
        [HttpGet("GetUsersByRole")]
        public async Task<IActionResult> GetUsersByRole([FromQuery] string? role = "")
        {

            var response = await service.GetUsersByRole(role);
            return response.Success ? Ok(response) : BadRequest(response);

        }

        [HttpGet("GetUserbyEmail")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var response = await service.GetUserByEmail(email);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        //-------------------------------------------------------------------------------------------

        [HttpPost("roles/grant/{userId}")]
        public async Task<IActionResult> GrantRole(string userId, string RoleName)
        {
            var response = await service.GrantUserRole(userId, RoleName);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost("roles/grantByEmail/{email}")]
        
        public async Task<IActionResult> GrantRolesByEmail(string email, string roleName)
        {
            var response = await service.GrantUserRoleByEmail(email, roleName);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        
        [HttpPost("roles/revoke/{userId}")]
        public async Task<IActionResult> RevokeRoles(string userId)
        {
            var response = await service.RevokeUserRoles(userId);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        
        [HttpPost("role/revoke/{userId}")]
        public async Task<IActionResult> RevokeRole(string userId, string roleName)
        {
            var response = await service.RevokeUserRole(userId, roleName);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet]
        [AllowAnonymous] // -----------------------------------------------------------------------------------------------------------> BORRAR
        public async Task<IActionResult> Get([FromQuery] string? rolId, [FromQuery] string? search, [FromQuery] PaginationDto pagination)
        {
            var response = await service.GetAsync(rolId, search, pagination);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPatch("{id}/finalize")]
        public async Task<IActionResult> Finalize(string id)
        {
            var response = await service.FinalizeAsync(id);

            if (!response.Success)
                return NotFound(response); // o BadRequest según el motivo

            return Ok(response);
        }

        [HttpPatch("{id}/initialize")]
        public async Task<IActionResult> Initialize(string id)
        {
            var response = await service.InitializeAsync(id);

            if (!response.Success)
                return NotFound(response); // o BadRequest según el motivo

            return Ok(response);
        }
    }
}
