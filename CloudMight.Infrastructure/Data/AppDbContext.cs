using CloudMight.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudMight.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) 
{
    public DbSet<Photo> Photos { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<HostEnviromentConfig> HostEnviromentConfigs { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>()
            .HasKey(u => u.Id);
        builder.Entity<User>()
            .HasOne(u => u.HostEnviromentConfig)
            .WithOne(hec => hec.User)
            .HasForeignKey<HostEnviromentConfig>(hec => hec.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<User>()
            .HasOne(u => u.RefreshToken)
            .WithOne(rt => rt.User)
            .HasForeignKey<RefreshToken>(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Photo>()
            .HasKey(p => p.Id);

        builder.Entity<HostEnviromentConfig>()
            .HasKey(hec => hec.Id);

        builder.Entity<RefreshToken>()
            .HasKey(rt => rt.Id);
    }
}