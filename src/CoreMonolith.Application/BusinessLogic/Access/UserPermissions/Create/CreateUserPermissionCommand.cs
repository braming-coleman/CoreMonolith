using CoreMonolith.Application.Abstractions.Messaging;

namespace CoreMonolith.Application.BusinessLogic.Access.UserPermissions.Create;

public sealed record CreateUserPermissionCommand(Guid UserId, Guid PermissionId)
    : ICommand<Guid>;
