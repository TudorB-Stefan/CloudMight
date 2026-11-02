using CloudMight.Api.DTOs.Authentification;
using CloudMight.Api.DTOs.User;
using CloudMight.Api.Extensions;
using CloudMight.Core.Entities;
using CloudMight.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudMight.Api.Controllers;

public class AuthController(IUserService userService, 
    IPasswordService passwordService,
    ITokenService tokenService) : BaseController
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
        var userResult = await userService.FindByEmailAsync(request.Email,new CancellationToken());
        if (!userResult.IsSuccess)
        {
            return Unauthorized("Invalid email or password.");
        }

        var user = userResult.Data;
        
        var passwordMatch = await userService.CheckPassword(
            request.Email,
            request.Password,
            cancellationToken
        );
        if (!passwordMatch.IsSuccess)
        {
            return Unauthorized("Invalid email or password.");
        }

        var token = await tokenService.CreateTokenAsync(user);
        var refreshToken = tokenService.CreateRefreshTokenAsync();

        user.RefreshToken = refreshToken;
        user.RefreshToken.ExpiresAt = DateTime.UtcNow.AddDays(2);
        await userService.UpdateAsync(user,cancellationToken);
        
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(2)
        };
        Response.Cookies.Append("auth_token",token,cookieOptions);
        
        return Ok(user.ToSelfResponse());
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<LoginResponseDto>> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto,CancellationToken cancellationToken)
    {
        var userResponse = await userService.FindByRefreshToken(refreshTokenDto.Token,cancellationToken);
        if (!userResponse.IsSuccess)
        {
            return Unauthorized();
        }

        var user = userResponse.Data;

        if (user.RefreshToken == null || user.RefreshToken.IsExpired)
        {
            user.RefreshToken = null;
            await userService.UpdateAsync(user, cancellationToken);
        }
        
        var newRefreshToken = tokenService.CreateRefreshTokenAsync();
        user.RefreshToken = newRefreshToken;
        user.RefreshToken.ExpiresAt = DateTime.UtcNow.AddDays(2);

        await userService.UpdateAsync(user, cancellationToken);
        var newToken = await tokenService.CreateTokenAsync(user);
        
        Response.Cookies.Append("auth_token",newToken,new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(2)
        });
        
        return Ok(user.ToSelfResponse());
    }
}