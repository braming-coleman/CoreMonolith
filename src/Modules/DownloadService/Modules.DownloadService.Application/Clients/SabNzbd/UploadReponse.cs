using System.Text.Json.Serialization;

namespace Modules.DownloadService.Application.Clients.SabNzbd;

public class UploadReponse
{
    [JsonPropertyName("status")]
    public bool Status { get; set; }


    [JsonPropertyName("nzo_ids")]
    public List<string> UploadIds { get; set; }
}
