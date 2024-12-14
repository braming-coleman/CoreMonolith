using CoreMonolith.Application.Abstractions.Messaging;

namespace CoreMonolith.Application.Access.UserPermissions.GetPermissionsByUserId;

public sealed record GetPermissionsByUserIdQuery(Guid UserId) : IQuery<HashSet<string>>;
