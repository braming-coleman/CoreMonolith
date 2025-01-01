using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Modules.DownloadService.Api.Usenet.SabNzbd.Models.Api;

public class PostRequest
{
    [FromQuery(Name = "output")]
    public string Output { get; set; }

    [FromQuery(Name = "apikey")]
    public string ApiKey { get; set; }

    [FromQuery(Name = "mode")]
    public string Mode { get; set; }

    //[FromForm(Name = "name")]
    public IFormFile Name { get; set; }

    [FromForm(Name = "nzbname")]
    public string? NzbName { get; set; }

    [FromQuery(Name = "pp")]
    public string? Pp { get; set; }

    [FromQuery(Name = "priority")]
    public string? Priority { get; set; }

    [FromQuery(Name = "cat")]
    public string? Cat { get; set; }
}