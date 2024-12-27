namespace Modules.DownloadService.Api.Usenet.SabNzbd.Models;

public sealed record NzbUploadResponse(bool Status, List<string> UploadIds);
