using CloudMight.Core.Entities;
using CloudMight.Core.Wrappers;
using Microsoft.AspNetCore.Identity.Data;

namespace CloudMight.Core.Interfaces.Services;

public interface IUserService
{
    Task<Result<User>> FindByEmailAsync(string email,CancellationToken ct);
    Task<Result<User>> FindByUsernamesync(string username,CancellationToken ct);
    Task<Result<User>> FindByRefreshToken(string refreshToken,CancellationToken ct);
    Task<Result> CreateLocalUser(string username, string plainTextPassword,CancellationToken ct);
    Task<Result> CreateLocalFolder(string username,CancellationToken ct);
    Task<Result<HostEnviromentConfig>> CreateUserConfig(string username, string plainTextPassword,CancellationToken ct);
    Task<Result> CreateUserAsync(User user,CancellationToken ct);
    Task<Result> UpdateAsync(User user, CancellationToken ct);
    Task<Result> CheckPassword(string email, string password,CancellationToken ct);
}