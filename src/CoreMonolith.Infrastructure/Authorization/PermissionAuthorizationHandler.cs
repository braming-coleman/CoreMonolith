using CoreMonolith.SharedKernel.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Modules.UserService.Api;

namespace CoreMonolith.Infrastructure.Authorization;

internal sealed class PermissionAuthorizationHandler(IUserServiceApi _userService)
    : AuthorizationHandler<PermissionRequirement>
{
    private readonly string[] _overridePaths = ["swagger", "auth-callback"];

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var pathValue = ((DefaultHttpContext)context.Resource!).Request.Path.Value!;

        if (pathValue.Contains(_overridePaths[0]) ||
            pathValue.Contains(_overridePaths[1]))
        {
            context.Succeed(requirement);

            return;
        }

        if (context.User is not { Identity.IsAuthenticated: true } or { Identity.IsAuthenticated: false })
            return;

        var externalId = context.User.GetExternalUserId();

        var permissions = await _userService.PermissionsGetByExternalIdAsync(externalId);

        if (permissions.Value.Contains(requirement.Permission))
        {
            context.Succeed(requirement);

            return;
        }
    }
}
