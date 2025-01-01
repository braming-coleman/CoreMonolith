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



    [FromQuery(Name = "start")]
    public int? Start { get; set; }

    [FromQuery(Name = "limit")]
    public int? Limit { get; set; }

    [FromQuery(Name = "category")]
    public string? Category { get; set; }
}
