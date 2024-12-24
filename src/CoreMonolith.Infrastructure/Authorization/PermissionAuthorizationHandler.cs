using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.SharedKernel.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace CoreMonolith.Infrastructure.Authorization;

internal sealed class PermissionAuthorizationHandler(IUnitOfWork _unitOfWork)
    : AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        //swagger short circuit
        if (((DefaultHttpContext)context.Resource!).Request.Path.Value!.Contains("swagger"))
        {
            context.Succeed(requirement); return;
        }

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
