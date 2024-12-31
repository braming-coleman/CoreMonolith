using System.Text.Json.Serialization;

namespace Modules.DownloadService.Api.Usenet.SabNzbd.Models.Api;

public record VersionResponse(
    [property: JsonPropertyName("version")] string Version
);
