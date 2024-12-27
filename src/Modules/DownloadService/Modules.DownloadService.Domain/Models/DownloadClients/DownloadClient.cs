using CoreMonolith.Domain.Models;

namespace Modules.DownloadService.Domain.Models.DownloadClients;

public class DownloadClient : Entity
{
    public Guid Id { get; set; }
    public DownloadClientType Type { get; set; }
    public string Name { get; set; }
    public string ConfigString { get; set; }
    public bool Enabled { get; set; }
}
