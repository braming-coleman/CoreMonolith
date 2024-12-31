using CoreMonolith.SharedKernel.Helpers;
using CoreMonolith.SharedKernel.ValueObjects;
using Modules.DownloadService.Api.Usenet.SabNzbd;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models.Api;
using Modules.DownloadService.Application.Clients.SabNzbd;
using Modules.DownloadService.Application.Clients.SabNzbd.Models;

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

    public async Task<Result<VersionResponse>> GetVerionAsync(GetRequest request, CancellationToken cancellationToken = default)
    {
        var requestString =
            $"{_setttings.BasePath}" +
            $"?output={_setttings.Output}" +
            $"&mode={request.Mode}" +
            $"&apikey={request.ApiKey}";

        var response = await _httpClient.GetAsync(requestString, cancellationToken);

        if (response is null || !response.IsSuccessStatusCode)
            return Result.Failure<VersionResponse>(SabNzbdClientErrors.GetFailure(response!.StatusCode.ToString()));

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

        var result = await JsonHelper.DeserializeAsync<VersionResponse>(responseContent);

        return result;
    }

    public async Task<Result<UploadReponse>> UploadNzbAsync(
        NzbUploadRequest request,
        CancellationToken cancellationToken = default)
    {
        var content = new MultipartFormDataContent
        {
            { new StringContent(_setttings.Output), "output" },
            { new StringContent(_setttings.ApiKey), "apikey" },
            { new StringContent(SabNzbdCommands.AddFile), "mode" },
            { new StreamContent(new MemoryStream(request.File)), "name", request.NzbName },
            { new StringContent(request.NzbName), "nzbname" },
            { new StringContent(request.Priority.ToString()), "priority" },
            { new StringContent(request.PostProcessing.ToString()), "pp" },
            { new StringContent(request.Category), "cat" },
        };

        var response = await _httpClient.PostAsync(_setttings.BasePath, content, cancellationToken);

        if (response is null || !response.IsSuccessStatusCode)
            return Result.Failure<UploadReponse>(SabNzbdClientErrors.UploadFailure(response!.StatusCode.ToString()));

        var contentString = await response.Content.ReadAsStringAsync(cancellationToken);

        var result = await JsonHelper.DeserializeAsync<UploadReponse>(contentString);

        return result;
    }
}
