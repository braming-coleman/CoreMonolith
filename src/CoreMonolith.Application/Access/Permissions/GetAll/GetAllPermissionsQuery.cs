using CoreMonolith.Application.Abstractions.Messaging;

namespace CoreMonolith.Application.Access.Permissions.GetAll;

public sealed record GetAllPermissionsQuery() : IQuery<List<PermissionReposnse>>;