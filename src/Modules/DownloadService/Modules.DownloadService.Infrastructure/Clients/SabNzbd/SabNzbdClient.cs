using CoreMonolith.SharedKernel.ValueObjects;
using Microsoft.Extensions.Options;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models;
using Modules.DownloadService.Application.Clients.SabNzbd;
using Modules.DownloadService.Infrastructure.Clients.SabNzbd.Models;
using System.Text.Json;

namespace Modules.DownloadService.Infrastructure.Clients.SabNzbd;

internal sealed class SabNzbdClient(
    HttpClient _httpClient,
    IOptions<SabNzbdClientSettings> _options)
    : ISabNzbdClient
{
    public async Task<Result<UploadReponse>> UploadNzbAsync(
        Guid downloadClientId,
        NzbUploadRequest request,
        CancellationToken cancellationToken = default)
    {
        var mode = "addfile";
        var settings = _options.Value;

        _httpClient.BaseAddress = new(settings.BaseAddress);

        var message = new HttpRequestMessage(HttpMethod.Post, SabNzbdClientSettings.BasePath)
        {
            Content = new MultipartFormDataContent
            {
                { new StringContent(SabNzbdClientSettings.Output), "output" },
                { new StringContent(settings.ApiKey), "apikey" },
                { new StringContent(mode), "mode" },
                { new StreamContent(new MemoryStream(request.File)), "name" },
                { new StringContent(string.IsNullOrEmpty(request.JobName) ? request.FileName : request.JobName), "nzbname" },
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
