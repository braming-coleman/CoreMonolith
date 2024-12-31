namespace Modules.DownloadService.Api.Models;

public sealed record DownloadClientCreateRequest(
    DownloadClientType ClientType,
    string Name,
    bool Enabled,
    object Config);
