using CoreMonolith.Application.Abstractions.Messaging;

namespace CoreMonolith.Application.BusinessLogic.UserPermissionGroups.GetPermissionsByUserId;

public sealed record GetPermissionsByUserIdQuery(Guid UserId) : IQuery<HashSet<string>>;
