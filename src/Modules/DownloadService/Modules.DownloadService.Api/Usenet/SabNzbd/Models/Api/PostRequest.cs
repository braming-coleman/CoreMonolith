using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Modules.DownloadService.Api.Usenet.SabNzbd.Models.Api;

public class PostRequest
{
    [FromForm(Name = "output")]
    public string Output { get; set; }

    [FromForm(Name = "apikey")]
    public string ApiKey { get; set; }

    [FromForm(Name = "mode")]
    public string Mode { get; set; }

    [FromForm(Name = "name")]
    public IFormFile Name { get; set; }

    [FromForm(Name = "nzbname")]
    public string NzbName { get; set; }

    [FromForm(Name = "pp")]
    public string Pp { get; set; }

    [FromForm(Name = "priority")]
    public string Priority { get; set; }

    [FromForm(Name = "cat")]
    public string Cat { get; set; }
}