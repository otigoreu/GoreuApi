﻿using Azure;
using Goreu.Dto.Request;
using Goreu.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Goreu.API.Controllers
{
    [ApiController]
    [Route("api/users")]
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
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var response = await service.LoginAsync(request);
            return response.Success ? Ok(response) : Unauthorized(response);
        }

        [HttpPost("RequestTokenToResetPassword")]
        public async Task<IActionResult> RequestTokenToResetPassword(ResetPasswordRequestDto request)
        {
            var response = await service.RequestTokenToResetPasswordAsync(request);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] NewPasswordRequestDto request)
        {
            var response = await service.ResetPasswordAsync(request);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost("ChangePassword")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

    }
}
