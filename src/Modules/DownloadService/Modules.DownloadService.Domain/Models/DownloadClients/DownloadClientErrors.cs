using CoreMonolith.SharedKernel.Errors;

namespace Modules.DownloadService.Domain.Models.DownloadClients;

public static class DownloadClientErrors
{
    public static Error ConfigExists(string clientType) => new(
        "DownloadClient.ConfigExists",
        $"Config for client type '{clientType}' already exists.",
        ErrorType.Validation);

    public static Error ConfigUnsupported(string clientType) => new(
        "DownloadClient.ConfigUnsupported",
        $"Config for client type '{clientType}' not supported.",
        ErrorType.Problem);

    public static Error ConfigFailed(string clientType) => new(
        "DownloadClient.ConfigFailed",
        $"Config for client type '{clientType}' failed to deserialize.",
        ErrorType.Failure);

    public static Error ConfigNotCorrectType(string clientType, string className) => new(
        "DownloadClient.ConfigNotCorrectType",
        $"Config object for client type '{clientType}' not correct type of '{className}'.",
        ErrorType.Validation);
}
