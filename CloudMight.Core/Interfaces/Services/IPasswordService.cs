namespace CloudMight.Core.Interfaces.Services;

public interface IPasswordService
{
    string HashPassword(string password,CancellationToken ct);
    bool VerifyPassword(string password, string storedHash,CancellationToken ct);
}