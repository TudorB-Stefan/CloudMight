using CloudMight.Api.DTOs.Authentification;
using CloudMight.Api.DTOs.HostEnviromentConfig;
using CloudMight.Api.DTOs.User;
using CloudMight.Core.Entities;

namespace CloudMight.Api.Extensions;

public static class UserExtensions
{
    public static User DtoToUser(this RegisterRequestDto dto,
        string hashedPassword,
        Guid hostEnviromentId)
    {
        return new User()
        {
            Id = Guid.NewGuid(),
            UserName = dto.UserName,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Password = hashedPassword,
            ProfilePictureKey = string.Empty,
            Role = UserRole.Client,
            HostEnviromentConfigId = hostEnviromentId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };
    }
    
    public static SelfResponseDto ToSelfResponse(this User user)
    {
        return new SelfResponseDto()
        {
            Id = user.Id,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Role = user.Role,
            HostEnviromentConfig = user.HostEnviromentConfig.ToHostEnviromentConfigDto(),
            CreatedAt = user.CreatedAt
        };
    }

    public static HostEnviromentConfigDto ToHostEnviromentConfigDto(this HostEnviromentConfig? config)
    {
        if (config == null)
        {
            return null;
        }
        return new HostEnviromentConfigDto()
        {
            Id = config.Id,
            LinuxUserName = config.LinuxUserName,
            WireguardPublicKey = config.WireguardPublicKey,
            AssignedTunnelIp = config.AssignedTunnelIp,
            QuotaLimitBytes = config.QuotaLimitBytes,
            CurrentUsageBytes = config.CurrentUsageBytes
        };
    }
}