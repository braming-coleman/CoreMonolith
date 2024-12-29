using CoreMonolith.SharedKernel.ValueObjects;
using Modules.DownloadService.Api.Usenet.SabNzbd;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models;
using Modules.DownloadService.Application.Clients.SabNzbd;
using Modules.DownloadService.Application.Clients.SabNzbd.Models;
using System.Text.Json;

namespace Modules.DownloadService.Infrastructure.Clients.SabNzbd;

internal sealed class SabNzbdClient(
    HttpClient _httpClient)
    : ISabNzbdClient
{
    SabNzbdClientSettings _setttings;

    public Task ConfigureAsync(SabNzbdClientSettings settings)
    {
        _setttings = settings;

        _httpClient.BaseAddress = new(settings.BaseAddress);

        return Task.CompletedTask;
    }

    public async Task<Result<UploadReponse>> UploadNzbAsync(
        NzbUploadRequest request,
        CancellationToken cancellationToken = default)
    {
        var message = new HttpRequestMessage(HttpMethod.Post, _setttings.BasePath)
        {
            Content = new MultipartFormDataContent
            {
                { new StringContent(_setttings.Output), "output" },
                { new StringContent(_setttings.ApiKey), "apikey" },
                { new StringContent(SabNzbdCommands.AddFile), "mode" },
                { new StreamContent(new MemoryStream(request.File)), "name" },
                { new StringContent(request.NzbName), "nzbname" },
                { new StringContent(request.Priority.ToString()), "priority" },
                { new StringContent(request.PostProcessing.ToString()), "pp" },
                { new StringContent(request.Category), "cat" },
            }
        };

        var response = await _httpClient.SendAsync(message, cancellationToken);

        if (response is null || !response.IsSuccessStatusCode)
            return Result.Failure<UploadReponse>(
                SabNzbdClientErrors.UploadFailure(response!.StatusCode.ToString()));

        var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken);

        var results = await JsonSerializer.DeserializeAsync<UploadReponse>(
            utf8Json: contentStream,
            cancellationToken: cancellationToken);

        return results;
    }
}
