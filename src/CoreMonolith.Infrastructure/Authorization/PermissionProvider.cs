using CoreMonolith.Application.BusinessLogic.Access.UserPermissions.GetPermissionsByUserId;
using MediatR;

namespace CoreMonolith.Infrastructure.Authorization;

internal sealed class PermissionProvider(
    ISender _sender)
{
    public async Task<HashSet<string>> GetForUserIdAsync(Guid userId)
    {
        var query = new GetPermissionsByUserIdQuery(userId);

        var result = await _sender.Send(query, default);

        return result.Value;
    }
}
