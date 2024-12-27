namespace Modules.DownloadService.Api.Usenet.SabNzbd.Models;

public sealed record NzbUploadRequest(
    string FileName,
    byte[] File,
    string JobName = "",
    string Category = "*",
    Priority Priority = Priority.Default,
    PostProcessingOptions PostProcessing = PostProcessingOptions.Default);
