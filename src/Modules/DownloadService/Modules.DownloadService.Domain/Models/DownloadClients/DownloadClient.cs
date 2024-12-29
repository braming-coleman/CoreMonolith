using CoreMonolith.Domain.Models;
using Modules.DownloadService.Api.Models;

namespace Modules.DownloadService.Domain.Models.DownloadClients;

public class DownloadClient : Entity
{
    public Guid Id { get; set; }
    public DownloadClientType Type { get; set; }
    public string Name { get; set; }
    public string ConfigString { get; set; }
    public bool Enabled { get; set; }
}
