using CloudMight.Api.DTOs.HostEnviromentConfig;
using CloudMight.Core.Entities;

namespace CloudMight.Api.DTOs.User;

public class SelfResponseDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public HostEnviromentConfigDto? HostEnviromentConfig { get; set; }
    public DateTime CreatedAt { get; set; }
}