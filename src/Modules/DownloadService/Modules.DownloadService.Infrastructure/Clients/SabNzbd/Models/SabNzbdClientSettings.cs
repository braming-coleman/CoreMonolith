using Modules.DownloadService.Api.Usenet.SabNzbd.Models;

namespace Modules.DownloadService.Infrastructure.Clients.SabNzbd.Models;

internal class SabNzbdClientSettings
{
    public static string BasePath => "/sabnzbd/api";
    public static string Output => "jsonrpc";
    public string BaseAddress => $"{Host}:{Port}";

    public string Host { get; set; }
    public int Port { get; set; }
    public string ApiKey { get; set; }
    public string Category { get; set; }
    public Priority Priority { get; set; }
    public PostProcessingOptions PostPorcesssing { get; set; }
}
