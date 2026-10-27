namespace CloudMight.Core.Interfaces;

public interface IGenericRepository<T> where T : notnull
{
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct);
    Task<T?> GetByIdAsync(string id,CancellationToken ct);
    Task AddAsync(T entity,CancellationToken ct);
    Task UpdateAsync(T entity,CancellationToken ct);
    Task DeleteAsync(string id,CancellationToken ct);
}