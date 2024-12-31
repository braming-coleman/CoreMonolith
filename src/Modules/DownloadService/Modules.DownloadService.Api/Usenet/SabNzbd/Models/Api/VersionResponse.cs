using System.Text.Json.Serialization;

namespace Modules.DownloadService.Api.Usenet.SabNzbd.Models.Api;

public class VersionResponse
{
    [JsonPropertyName("version")]
    public string Version { get; set; }
}
