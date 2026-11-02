using CloudMight.Core.Entities;
using CloudMight.Core.Wrappers;

namespace CloudMight.Core.Interfaces.Services;

public interface ITokenService
{
    Task<string> CreateTokenAsync(User user);
    RefreshToken CreateRefreshTokenAsync();
}