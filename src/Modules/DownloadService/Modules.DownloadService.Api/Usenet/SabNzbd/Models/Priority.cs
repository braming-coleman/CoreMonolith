namespace Modules.DownloadService.Api.Usenet.SabNzbd.Models;

public enum Priority
{
    Default = -100,
    Duplicate = -3,
    Paused = -2,
    Low = -1,
    Normal = 0,
    High = 1,
    Force = 2
}
