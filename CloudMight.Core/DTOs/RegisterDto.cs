using System.ComponentModel.DataAnnotations;

namespace CloudMight.Core.DTOs;

public class RegisterDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    [Required(ErrorMessage = "Username is required")]
    public string? Username { get; set; }
    [Required(ErrorMessage = "Email is required")]
    public string? Email { get; set; }
    public string? ProfilePictureUrl { get; set; } = "/images/default-avatar.jpg";
    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }

    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string? ConfirmPassword { get; set; } = null;
}