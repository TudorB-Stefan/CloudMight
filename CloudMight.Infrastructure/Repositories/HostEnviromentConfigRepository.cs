using CloudMight.Core.Entities;
using CloudMight.Core.Interfaces.Repositories;
using CloudMight.Infrastructure.Data;

namespace CloudMight.Infrastructure.Repositories;

public class HostEnviromentConfigRepository(AppDbContext context) : IHostEnviromentConfigRepository
{
    public async Task AddAsync(HostEnviromentConfig config, CancellationToken cancellationToken)
    {
        await context.HostEnviromentConfigs.AddAsync(config, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}