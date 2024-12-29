using Modules.DownloadService.Application.Clients.SabNzbd.Models;
using Modules.DownloadService.Domain.Models.DownloadClients;
using System.Text;
using System.Text.Json;

namespace Modules.DownloadService.Application.Clients.SabNzbd;

public static class DownloadClientExtentions
{
    public static async Task<SabNzbdClientSettings?> GetConfigAsync(
        this DownloadClient downloadClient,
        CancellationToken cancellationToken = default)
    {
        var byteArray = Encoding.UTF8.GetBytes(downloadClient.ConfigString);
        using var stream = new MemoryStream(byteArray);

        return await JsonSerializer.DeserializeAsync<SabNzbdClientSettings>(stream, cancellationToken: cancellationToken);
    }

    public static async Task SetConfigAsync(
        this DownloadClient downloadClient,
        SabNzbdClientSettings config,
        CancellationToken cancellationToken = default)
    {
        using var resultStream = new MemoryStream();

        await JsonSerializer.SerializeAsync(resultStream, config, cancellationToken: cancellationToken);

        using var reader = new StreamReader(resultStream);

        downloadClient.ConfigString = await reader.ReadToEndAsync();
    }
}
