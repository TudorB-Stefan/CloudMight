namespace CloudMight.Core.Entities;

public class HostEnviromentConfig
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public User? User { get; set; }

    public string LinuxUserName { get; set; } = string.Empty;

    public string WireguardPublicKey { get; set; } = string.Empty;
    public string AssignedTunnelIp { get; set; } = string.Empty;

    public long QuotaLimitBytes { get; set; }
    public long CurrentUsageBytes { get; set; }
}