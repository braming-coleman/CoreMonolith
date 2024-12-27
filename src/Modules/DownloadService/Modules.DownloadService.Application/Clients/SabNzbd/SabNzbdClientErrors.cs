using CoreMonolith.SharedKernel.Errors;

namespace Modules.DownloadService.Application.Clients.SabNzbd;

public static class SabNzbdClientErrors
{
    public static Error UploadFailure(string status) => new(
        "SabNzbdClient.UploadFailure",
        $"Upload failed with status: '{status}'.",
        ErrorType.Failure);
}
