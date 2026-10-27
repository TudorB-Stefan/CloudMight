using CloudMight.Core.Entities;

namespace CloudMight.Core.Interfaces.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> FindUserByEmailAsync(string email,CancellationToken ct);
    Task<User?> FindUserByUsernameAsync(string username,CancellationToken ct);
    Task<User?> FindUserByRefreshTokenAsync(string token,CancellationToken ct);
}