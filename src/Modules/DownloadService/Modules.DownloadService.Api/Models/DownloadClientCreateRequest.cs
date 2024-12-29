using CoreMonolith.Application.Abstractions.Idempotency;

namespace Modules.DownloadService.Api.Models;

public sealed record DownloadClientCreateRequest(
    Guid RequestId,
    DownloadClientType ClientType,
    string Name,
    bool Enabled,
    object Config) : IdempotentCommand<Guid>(RequestId);
