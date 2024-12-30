using CoreMonolith.SharedKernel.Errors;

namespace Modules.DownloadService.Domain.Models.DownloadClients;

public static class DownloadClientErrors
{
    public static Error ConfigNotCorrectType(string clientType, string className) => new(
        "DownloadClient.ConfigNotCorrectType",
        $"Config object for client type '{clientType}' not correct type of '{className}'.",
        ErrorType.Validation);
}
