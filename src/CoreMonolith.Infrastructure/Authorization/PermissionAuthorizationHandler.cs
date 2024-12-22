using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.SharedKernel.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace CoreMonolith.Infrastructure.Authorization;

internal sealed class PermissionAuthorizationHandler(IUnitOfWork _unitOfWork)
    : AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        if (context.User is not { Identity.IsAuthenticated: true } or { Identity.IsAuthenticated: false })
            return;

        var externalId = context.User.GetExternalUserId();

        var permissions = await _unitOfWork.Access
            .UserPermissionGroupRepository.GetPermissionsByExternalId(externalId);

        if (permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);

            return;
        }
    }
}
