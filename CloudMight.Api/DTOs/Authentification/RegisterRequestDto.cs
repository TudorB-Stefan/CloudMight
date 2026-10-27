using System.ComponentModel.DataAnnotations;

namespace CloudMight.Api.DTOs.Authentification;

public class RegisterRequestDto
{
    [Required] public string Email { get; set; } = string.Empty;
    [Required] public string UserName { get; set; } = string.Empty;
    [Required] public string FirstName { get; set; } = string.Empty;
    [Required] public string LastName { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
    [Compare("Password",ErrorMessage = "Passwords do not match.")]
    [Required] public string ConfirmPassword { get; set; } = string.Empty;
}