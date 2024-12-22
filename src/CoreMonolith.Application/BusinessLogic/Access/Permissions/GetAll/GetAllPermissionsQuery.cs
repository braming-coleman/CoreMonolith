using CoreMonolith.Application.Abstractions.Messaging;

namespace CoreMonolith.Application.BusinessLogic.Access.Permissions.GetAll;

public sealed record GetAllPermissionsQuery() : IQuery<List<PermissionReposnse>>;