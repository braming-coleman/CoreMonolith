using Microsoft.AspNetCore.Authorization;

namespace CoreMonolith.Infrastructure.Authorization;

internal sealed class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}
