using CoreMonolith.Application.Abstractions.Messaging;

namespace CoreMonolith.Application.BusinessLogic.UserPermissionGroups.GetPermissionsByExternalId;

public sealed record GetPermissionsByExternalIdQuery(Guid ExternalId) : IQuery<HashSet<string>>;
