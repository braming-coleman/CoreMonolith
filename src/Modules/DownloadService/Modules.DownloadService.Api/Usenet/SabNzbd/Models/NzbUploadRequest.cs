namespace Modules.DownloadService.Api.Usenet.SabNzbd.Models;

public sealed record NzbUploadRequest(
    string ApiKey,
    byte[] File,
    string NzbName,
    string Category,
    Priority Priority,
    PostProcessingOptions PostProcessing);
