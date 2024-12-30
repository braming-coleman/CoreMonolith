using System.Text.Json.Serialization;

namespace Modules.DownloadService.Api.Usenet.SabNzbd.Models;

public class NzbUploadResponse
{
    [JsonPropertyName("status")]
    public bool Status { get; set; }


    [JsonPropertyName("nzo_ids")]
    public List<string> UploadIds { get; set; }


    [JsonPropertyName("error")]
    public string Error { get; set; }
}