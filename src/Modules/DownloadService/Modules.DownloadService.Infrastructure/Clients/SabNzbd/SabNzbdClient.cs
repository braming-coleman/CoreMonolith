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
        var queryPath = BuildClientRequest(request.Mode, settings);

        var response = await _httpClient.GetAsync(queryPath, cancellationToken);

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
        var queries = new Dictionary<string, string?>
        {
            { "priority", request.Priority.ToString() },
            { "pp", request.PostProcessing.ToString() },
            { "cat", request.Category }
        };

        var queryPath = BuildClientRequest(SabNzbdCommands.AddFile, settings, queries);

        var content = new MultipartFormDataContent
        {
            { new StreamContent(new MemoryStream(request.File)), "name", request.NzbName },
            { new StringContent(request.NzbName), "nzbname" }
        };

        var response = await _httpClient.PostAsync(queryPath, content, cancellationToken);

        if (response is null || !response.IsSuccessStatusCode)
            return Result.Failure<UploadReponse>(SabNzbdClientErrors.UploadFailure(response!.StatusCode.ToString()));

        var contentString = await response.Content.ReadAsStringAsync(cancellationToken);

        var result = await JsonHelper.DeserializeAsync<UploadReponse>(contentString);

        return result;
    }

    private string BuildClientRequest(string mode, SabNzbdClientSettings settings, Dictionary<string, string?>? additionalParams = default)
    {
        _httpClient.BaseAddress = new(settings.BaseAddress);

        var queries = new Dictionary<string, string?>
        {
            { "output", settings.Output },
            { "apikey", settings.ApiKey },
            { "mode", mode }
        };

        if (additionalParams is not null)
            foreach (var item in additionalParams)
                queries.Add(item.Key, item.Value);

        return QueryHelpers.AddQueryString(settings.BasePath, queries);
    }
}
