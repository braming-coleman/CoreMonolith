using CoreMonolith.SharedKernel.Helpers;
using CoreMonolith.SharedKernel.ValueObjects;
using Microsoft.AspNetCore.WebUtilities;
using Modules.DownloadService.Api.Usenet.SabNzbd;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models.Api;
using Modules.DownloadService.Application.Clients.SabNzbd;
using Modules.DownloadService.Application.Clients.SabNzbd.Models;

namespace Modules.DownloadService.Infrastructure.Clients.SabNzbd;

internal sealed class SabNzbdClient(HttpClient _httpClient)
    : ISabNzbdClient
{
    public async Task<Result<T>> GetAsync<T>(GetRequest request, SabNzbdClientSettings settings, CancellationToken cancellationToken = default)
    {
        _httpClient.BaseAddress = new(settings.BaseAddress);

        var queries = new Dictionary<string, string?>
        {
            { "output", settings.Output },
            { "apikey", request.ApiKey },
            { "mode", request.Mode }
        };

        var queryString = QueryHelpers.AddQueryString(settings.BasePath, queries);

        var response = await _httpClient.GetAsync(queryString, cancellationToken);

        if (response is null || !response.IsSuccessStatusCode)
            return Result.Failure<T>(SabNzbdClientErrors.GetFailure(response!.StatusCode.ToString()));

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

        var result = await JsonHelper.DeserializeAsync<T>(responseContent);

        return result;
    }

    public async Task<Result<UploadReponse>> UploadNzbAsync(
        NzbUploadRequest request,
        SabNzbdClientSettings settings,
        CancellationToken cancellationToken = default)
    {
        _httpClient.BaseAddress = new(settings.BaseAddress);

        var content = new MultipartFormDataContent
        {
            { new StringContent(settings.Output), "output" },
            { new StringContent(settings.ApiKey), "apikey" },
            { new StringContent(SabNzbdCommands.AddFile), "mode" },
            { new StreamContent(new MemoryStream(request.File)), "name", request.NzbName },
            { new StringContent(request.NzbName), "nzbname" },
            { new StringContent(request.Priority.ToString()), "priority" },
            { new StringContent(request.PostProcessing.ToString()), "pp" },
            { new StringContent(request.Category), "cat" },
        };

        var response = await _httpClient.PostAsync(settings.BasePath, content, cancellationToken);

        if (response is null || !response.IsSuccessStatusCode)
            return Result.Failure<UploadReponse>(SabNzbdClientErrors.UploadFailure(response!.StatusCode.ToString()));

        var contentString = await response.Content.ReadAsStringAsync(cancellationToken);

        var result = await JsonHelper.DeserializeAsync<UploadReponse>(contentString);

        return result;
    }
}
