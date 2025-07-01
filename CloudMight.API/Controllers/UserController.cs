using System.Security.Claims;
using CloudMight.Core.DTOs;
using CloudMight.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CloudMight.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UserController(SignInManager<User> signInManager) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterDto registerDto)
    {
        var user = new User()
        {
            UserName = registerDto.Username,
            Email = registerDto.Email,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName
        };
        var result = await signInManager.UserManager.CreateAsync(user, registerDto.Password);
        if(result.Succeeded)
            return ValidationProblem();
        await signInManager.SignInAsync(user, false);
        return Ok();
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return Ok();
    }

    [HttpGet("user-info")]
    public async Task<ActionResult> GetUserInfo()
    {
        if(User.Identity?.IsAuthenticated == false) return NoContent();
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        var user = await signInManager.UserManager.FindByEmailAsync(email);
        
        return Ok(new
        {
            user.FirstName,
            user.LastName,
            user.UserName,
            user.Email,
            user.Id
        });
    }

    [HttpGet("auth-status")]
    public ActionResult AuthStatus()
    {
        return Ok(new
        {
            IsAuthenticated = User.Identity?.IsAuthenticated ?? false
        });
    }

    [HttpGet("all-users")]
    public async Task<ActionResult<IReadOnlyList<User>>> GetAllUsers()
    {
        var users = await signInManager.UserManager.Users.ToListAsync();
        return Ok(users.Select(temp => new
        {
            temp.FirstName,
            temp.LastName,
        }).ToList());
    }
}