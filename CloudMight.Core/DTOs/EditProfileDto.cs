using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CloudMight.Core.DTOs;

public class EditProfileDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public IFormFile? ProfilePicture { get; set; }
}