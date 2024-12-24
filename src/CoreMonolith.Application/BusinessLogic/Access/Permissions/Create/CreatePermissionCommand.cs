using CoreMonolith.Application.Abstractions.Idempotency;

namespace CoreMonolith.Application.BusinessLogic.Access.Permissions.Create;

public sealed record CreatePermissionCommand(
    Guid RequestId,
    string Key,
    string Description) : IdempotentCommand<Guid>(RequestId);
