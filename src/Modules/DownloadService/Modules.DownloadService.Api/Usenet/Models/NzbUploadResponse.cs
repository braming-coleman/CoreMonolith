namespace Modules.DownloadService.Api.Usenet.Models;

public sealed record NzbUploadResponse(bool Status, List<string> UploadIds);
