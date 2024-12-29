using Modules.DownloadService.Api.Usenet.SabNzbd.Models;

namespace Modules.DownloadService.Application.Clients.SabNzbd.Models;

public class SabNzbdClientSettings
{
    public string BasePath => "/sabnzbd/api";
    public string Output => "jsonrpc";
    public string BaseAddress => $"{Host}:{Port}";

    public string Host { get; set; }
    public int Port { get; set; }
    public string ApiKey { get; set; }
    public string Category { get; set; }
    public Priority Priority { get; set; }
    public PostProcessingOptions PostPorcesssing { get; set; }
}
