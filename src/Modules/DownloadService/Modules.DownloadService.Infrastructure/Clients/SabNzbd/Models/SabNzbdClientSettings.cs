namespace Modules.DownloadService.Infrastructure.Clients.SabNzbd.Models;

internal class SabNzbdClientSettings
{
    public string ApiPath => "/sabnzbd/api";
    public string Output => "jsonrpc";
    public string ApiKey { get; set; }
}
