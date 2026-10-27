using System.ComponentModel.DataAnnotations;

namespace CloudMight.Api.DTOs.Authentification;

public class LoginRequestDto
{
    [Required] public string Email { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
}