using Microsoft.AspNetCore.Mvc;

namespace Modules.DownloadService.Api.Usenet.SabNzbd.Models.Api;

public class GetRequest
{
    [FromQuery(Name = "output")]
    public string Output { get; set; }

    [FromQuery(Name = "apikey")]
    public string ApiKey { get; set; }

    [FromQuery(Name = "mode")]
    public string Mode { get; set; }
}
