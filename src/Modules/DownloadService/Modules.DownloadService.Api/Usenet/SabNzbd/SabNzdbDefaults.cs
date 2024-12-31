using Modules.DownloadService.Api.Usenet.SabNzbd.Models;

namespace Modules.DownloadService.Api.Usenet.SabNzbd;

public static class SabNzdbDefaults
{
    public static string Category => "*";
    public static Priority Priority => Priority.Default;
    public static PostProcessingOptions PostProcessingOptions => PostProcessingOptions.Default;
}
