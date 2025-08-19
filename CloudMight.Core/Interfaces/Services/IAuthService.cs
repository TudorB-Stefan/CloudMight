using CloudMight.Core.DTOs;

namespace CloudMight.Core.Interfaces.Services;

public interface IAuthService
{
    Task<string> LoginAsync(LoginDto dto);
}