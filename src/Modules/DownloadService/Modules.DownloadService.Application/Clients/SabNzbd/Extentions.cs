using CoreMonolith.SharedKernel.Helpers;
using Modules.DownloadService.Application.Clients.SabNzbd.Models;
using Modules.DownloadService.Domain.Models.DownloadClients;

namespace Modules.DownloadService.Application.Clients.SabNzbd;

public static class Extentions
{
    public static async Task<SabNzbdClientSettings?> GetConfigAsync(this DownloadClient downloadClient)
    {
        return await JsonHelper.DeserializeAsync<SabNzbdClientSettings>(downloadClient.ConfigString);
    }

    public static async Task SetConfigAsync(
        this DownloadClient downloadClient,
        SabNzbdClientSettings config)
    {
        downloadClient.ConfigString = await JsonHelper.SerializeAsync(config);
    }
}
