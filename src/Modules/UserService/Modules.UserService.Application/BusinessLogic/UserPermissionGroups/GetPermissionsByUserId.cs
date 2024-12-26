using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.SharedKernel.ValueObjects;
using Modules.UserService.Domain.Abstractions.Repositories;

namespace Modules.UserService.Application.BusinessLogic.UserPermissionGroups.GetPermissionsByUserId;

public sealed record GetPermissionsByUserIdQuery(Guid UserId) : IQuery<HashSet<string>>;

internal sealed class GetPermissionsByUserIdQueryHandler(IUserPermissionGroupRepository _userGroupRepo)
    : IQueryHandler<GetPermissionsByUserIdQuery, HashSet<string>>
{
    public async Task<Result<HashSet<string>>> Handle(GetPermissionsByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await _userGroupRepo.GetPermissionsByUserId(request.UserId, cancellationToken);
    }
}
