using CloudMight.Core.Entities;
using CloudMight.Core.Interfaces.Services;

namespace CloudMight.Infrastructure.Services;

public class TokentService : ITokenService
{
    public Task<string> CreateTokenAsync(User user)
    {
        throw new NotImplementedException();
    }

    public RefreshToken CreateRefreshTokenAsync()
    {
        throw new NotImplementedException();
    }
}