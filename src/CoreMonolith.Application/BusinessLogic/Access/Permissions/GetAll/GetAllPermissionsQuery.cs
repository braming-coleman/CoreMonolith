using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.Application.BusinessLogic.Access.Permissions;

namespace CoreMonolith.Application.BusinessLogic.Access.Permissions.GetAll;

public sealed record GetAllPermissionsQuery() : IQuery<List<PermissionReposnse>>;