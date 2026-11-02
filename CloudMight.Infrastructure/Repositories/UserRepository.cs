using CloudMight.Core.Entities;
using CloudMight.Core.Interfaces.Repositories;
using CloudMight.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CloudMight.Infrastructure.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task<IReadOnlyList<User>> GetAllAsync()
    {
        return await context.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(string id)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Id.ToString() == id);
    }

    public async Task AddAsync(User entity)
    {
        await context.Users.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User entity)
    {
        context.Users.Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string id)
    {
        var user = await GetByIdAsync(id);
        if (user == null) throw new KeyNotFoundException($"User with ID {id} was not found.");
        context.Users.Remove(user);
        await context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<User>> GetAllAsync(CancellationToken ct)
    {
        var users = await context.Users.ToListAsync(ct);
        return users;
    }

    public async Task<User?> GetByIdAsync(string id, CancellationToken ct)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id.ToString() == id,ct);
        return user;
    }

    public async Task AddAsync(User entity, CancellationToken ct)
    {
        await context.Users.AddAsync(entity, ct);
        await context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(User entity, CancellationToken ct)
    {
        context.Users.Update(entity);
        await context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(string id, CancellationToken ct)
    {
        var user = await GetByIdAsync(id, ct);
        if(user == null)
        {
            return;
        }
        
        context.Users.Remove(user);
        await context.SaveChangesAsync(ct);
    }

    public async Task<User?> FindUserByEmailAsync(string email, CancellationToken ct)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);
        return user;
    }

    public async Task<User?> FindUserByUsernameAsync(string username, CancellationToken ct)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        return user;
    }

    public async Task<User?> FindUserByRefreshTokenAsync(string token, CancellationToken ct)
    {
        var refreshToken = await context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token);
        var user = refreshToken.User;
        return user;
    }
}