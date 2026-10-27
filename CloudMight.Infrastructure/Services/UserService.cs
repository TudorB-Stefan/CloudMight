using CliWrap;
using CloudMight.Core.Entities;
using CloudMight.Core.Interfaces.Repositories;
using CloudMight.Core.Interfaces.Services;
using CloudMight.Core.Wrappers;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CloudMight.Infrastructure.Services;

public class UserService(IUserRepository userRepository,
    IPasswordService passwordService,
    IHostEnviromentConfigRepository hostEnviromentConfigRepository) : IUserService
{
    public async Task<Result<User>> FindByEmailAsync(string email,CancellationToken ct)
    {
        var user = await userRepository.FindUserByEmailAsync(email,ct);
        if (user == null)
            return Result<User>.Failure("No user was found!");
        return Result<User>.Success(user);
    }

    public async Task<Result<User>> FindByUsernamesync(string username,CancellationToken ct)
    {
        var user = await userRepository.FindUserByUsernameAsync(username,ct);
        if (user == null)
            return Result<User>.Failure("No user was found!");
        return Result<User>.Success(user);
    }

    public async Task<Result<User>> FindByRefreshToken(string refreshToken,CancellationToken ct)
    {
        var user = await userRepository.FindUserByRefreshTokenAsync(refreshToken,ct);
        if (user == null)
            return Result<User>.Failure("No user was found!");
        return Result<User>.Success(user);
    }

    public async Task<Result> CreateLocalUser(string username, string plainTextPassword,CancellationToken ct)
    {
        try
        {
            await Cli.Wrap("sudo")
                .WithArguments(new[] { "useradd", "-m", "-d", $"/home/CloudMightStorage/{username}", "-s", "/usr/bin/nologin", "-G", "cloudusers", username })
                .ExecuteAsync(ct);

            await Cli.Wrap("sudo")
                .WithArguments(new[] { "smbpasswd", "-a", "-s", username })
                .WithStandardInputPipe(PipeSource.FromString($"{plainTextPassword}\n{plainTextPassword}\n"))
                .ExecuteAsync(ct);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Unable to create the local user: {ex.Message}");
        }
    }

    public async Task<Result> CreateLocalFolder(string username,CancellationToken ct)
    {
        try
        {
            var targetDir = $"/home/CloudMightStorage/{username}";
            
            await Cli.Wrap("sudo")
                .WithArguments(new[] { "chown", $"{username}:cloudusers", targetDir })
                .ExecuteAsync(ct);

            await Cli.Wrap("sudo")
                .WithArguments(new[] { "chmod", "770", targetDir })
                .ExecuteAsync(ct);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Unable to secure the local folder: {ex.Message}");
        }
    }

    public async Task<Result<HostEnviromentConfig>> CreateUserConfig(string username, string plainTextPassword,CancellationToken ct)
    {
        var localUser = await CreateLocalUser(username, plainTextPassword, ct); 
        if (!localUser.IsSuccess)
        {
            return Result<HostEnviromentConfig>.Failure("Unable to create the local user.");
        }
        
        var localFolder = await CreateLocalFolder(username,ct); 
        if (!localFolder.IsSuccess)
        {
            return Result<HostEnviromentConfig>.Failure("Unable to create the local folder.");
        }
        
        var config = new HostEnviromentConfig
        {
            Id = Guid.NewGuid(),
            LinuxUserName = username,
            WireguardPublicKey = string.Empty, 
            AssignedTunnelIp = string.Empty,   
            QuotaLimitBytes = 5L * 1024 * 1024 * 1024,
            CurrentUsageBytes = 0
        };
        await hostEnviromentConfigRepository.AddAsync(config, ct);
        return Result<HostEnviromentConfig>.Success(config);
    }

    public async Task<Result> CreateUserAsync(User user,CancellationToken ct)
    {
        await userRepository.AddAsync(user,ct);
        return Result.Success();
    }

    public async Task<Result> CheckPassword(string email, string password,CancellationToken ct)
    {
        var user = await FindByEmailAsync(email,ct);
        
        if (!user.IsSuccess)
        {
            return Result.Failure("Invalid user.");
        }
        
        var result = passwordService.VerifyPassword(password, user.Data.Password,ct);

        if (!result)
        {
            return Result.Failure("The password is wrong.");
        }

        return Result.Success();
    }
}