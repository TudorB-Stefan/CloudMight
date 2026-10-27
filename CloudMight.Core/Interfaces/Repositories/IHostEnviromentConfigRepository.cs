using CloudMight.Core.Entities;

namespace CloudMight.Core.Interfaces.Repositories;

public interface IHostEnviromentConfigRepository
{
    Task AddAsync(HostEnviromentConfig config, CancellationToken cancellationToken);
}