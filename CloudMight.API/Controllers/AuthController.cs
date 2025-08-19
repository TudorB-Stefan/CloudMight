using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CloudMight.Core.DTOs;
using CloudMight.Core.Entities;
using CloudMight.Core.Interfaces.Services;
using CloudMight.Infrastructure.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace CloudMight.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;
    // private readonly IAuthService _authService;
    
    public AuthController(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        // IAuthService authService,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        // _authService = authService;
        _configuration = configuration;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var user = new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            UserName = dto.Username,
            ProfilePictureUrl = dto.ProfilePictureUrl
        };
        var result = await _userManager.CreateAsync(user, dto.Password);
        if(!result.Succeeded)
            return BadRequest(result.Errors);
        await _signInManager.SignInAsync(user, false);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if(user == null) return Unauthorized();
        var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
        if(!result.Succeeded) return Unauthorized();
        var token = GenerateJwtToken(user);
        return Ok(new { token });
    }
    [Authorize]
    [HttpGet("user-info")]
    public async Task<ActionResult> GetUserInfo()
    {
        if(User.Identity?.IsAuthenticated == false) return NoContent();
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        var user = await _signInManager.UserManager.FindByEmailAsync(email);
        
        return Ok(new
        {
            user.FirstName,
            user.LastName,
            user.UserName,
            user.Email,
            user.ProfilePictureUrl,
            user.Id
        });
    }
    [Authorize]
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
        var users = await _signInManager.UserManager.Users.ToListAsync();
        return Ok(users.Select(temp => new
        {
            temp.FirstName,
            temp.LastName,
            temp.UserName,
            temp.Email,
            temp.ProfilePictureUrl
        }).ToList());
    }
    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            // new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            // new Claim(JwtRegisteredClaimNames.Email, user.Email)
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:ExpireMinutes"])),
            signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}