using CloudMight.Core.DTOs;

namespace CloudMight.Core.Interfaces.Services;

public interface IAuthService
{
    Task<LoginResultDto> LoginAsync(LoginDto loginDto);
    Task<LoginResultDto> RegisterAsync(RegisterDto registerDto);
}