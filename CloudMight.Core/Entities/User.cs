using System.Runtime.InteropServices.JavaScript;

namespace CloudMight.Core.Entities;

public class User
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ProfilePictureKey { get; set; } = string.Empty;

    public UserRole Role { get; set; }

    public Guid HostEnviromentConfigId { get; set; } 
    public HostEnviromentConfig? HostEnviromentConfig { get; set; }

    public Guid RefreshTokenId { get; set; }
    public RefreshToken? RefreshToken { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public enum UserRole
{
    Admin,
    Client
}