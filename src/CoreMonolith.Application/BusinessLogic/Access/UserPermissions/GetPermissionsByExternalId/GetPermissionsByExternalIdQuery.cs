using CoreMonolith.Application.Abstractions.Messaging;

namespace CoreMonolith.Application.BusinessLogic.Access.UserPermissions.GetPermissionsByExternalId;

public sealed record GetPermissionsByExternalIdQuery(Guid ExternalId) : IQuery<HashSet<string>>;
