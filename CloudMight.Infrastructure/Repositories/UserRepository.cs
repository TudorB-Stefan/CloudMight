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
}