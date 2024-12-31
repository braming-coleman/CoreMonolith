namespace Modules.DownloadService.Api.Abstractions;

public interface IClientSettings
{
    string Host { get; set; }
    int Port { get; set; }
    string BaseAddress { get; }
}
