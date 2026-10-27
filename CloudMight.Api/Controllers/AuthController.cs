using CloudMight.Api.DTOs.Authentification;
using CloudMight.Api.DTOs.User;
using CloudMight.Api.Extensions;
using CloudMight.Core.Entities;
using CloudMight.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudMight.Api.Controllers;

public class AuthController(IUserService userService, 
    IPasswordService passwordService) : BaseController
{
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody]RegisterRequestDto request, CancellationToken cancellationToken)
    {
        var existingEmail = await userService.FindByEmailAsync(request.Email,cancellationToken);
        if (!existingEmail.IsSuccess)
        {
            return BadRequest("The email is already in use.");
        }

        var existingUsername = await userService.FindByUsernamesync(request.UserName,cancellationToken);
        if (!existingUsername.IsSuccess)
        {
            return BadRequest("The username is already in use.");
        }

        var hashedPassword = passwordService.HashPassword(request.Password,cancellationToken);

        var hostEnviromentConfig = await userService.CreateUserConfig(
            request.UserName,
            request.Password,
            cancellationToken
        );
        if (!hostEnviromentConfig.IsSuccess)
        {
            return BadRequest("The user config was not created.");
        }
        
        var user = request.DtoToUser(
            hashedPassword,
            hostEnviromentConfig.Data.Id
        );

        var result = await userService.CreateUserAsync(user,cancellationToken);
        if (!result.IsSuccess)
        {
            return BadRequest(new { Error = result.Errors } );
        }

        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto request,CancellationToken cancellationToken)
    {
        var user = await userService.FindByEmailAsync(request.Email,new CancellationToken());
        if (!user.IsSuccess)
        {
            return BadRequest("Invalid email or password.");
        }

        var passwordMatch = await userService.CheckPassword(
            request.Email,
            request.Password,
            cancellationToken
        );
        if (!passwordMatch.IsSuccess)
        {
            return BadRequest("Invalid email or password.");
        }

        return Ok();
    }
}