using CoreMonolith.SharedKernel.Errors;

namespace Modules.DownloadService.Api.Usenet.SabNzbd;

public static class SabNzbdClientErrors
{
    public static Error UploadFailure(string status) => new(
        "SabNzbdClient.UploadFailure",
        $"Upload failed with status: '{status}'.",
        ErrorType.Failure);

    public static Error GetFailure(string status) => new(
        "SabNzbdClient.GetFailure",
        $"Get failed with status: '{status}'.",
        ErrorType.Failure);

    public static Error NullClientResponse => new(
        "SabNzbdClient.NullClientResponse",
        $"Null resposne from client.",
        ErrorType.NullResult);

    public static Error ConfigNotFound => new(
        "SabNzbdClient.ConfigNotFound",
        $"No client config found.",
        ErrorType.NullResult);

    public static Error ApiKeyMismatch => new(
        "SabNzbdClient.ApiKeyMismatch",
        $"'apikey' does not match config.",
        ErrorType.Validation);

    public static Error ModeUnsupported(string mode) => new(
        "SabNzbdClient.ModeUnsupported",
        $"'mode': '{mode}' not supported",
        ErrorType.Validation);
}
