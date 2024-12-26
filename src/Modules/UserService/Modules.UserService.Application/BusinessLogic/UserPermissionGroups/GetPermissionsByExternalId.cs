using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.SharedKernel.ValueObjects;
using Modules.UserService.Domain.Abstractions.Repositories;

namespace Modules.UserService.Application.BusinessLogic.UserPermissionGroups.GetPermissionsByExternalId;

public sealed record GetPermissionsByExternalIdQuery(Guid ExternalId) : IQuery<HashSet<string>>;

internal sealed class GetPermissionsByExternalIdQueryHandler(IUserPermissionGroupRepository _userGroupRepo)
    : IQueryHandler<GetPermissionsByExternalIdQuery, HashSet<string>>
{
    public async Task<Result<HashSet<string>>> Handle(GetPermissionsByExternalIdQuery request, CancellationToken cancellationToken)
    {
        return await _userGroupRepo.GetPermissionsByExternalId(request.ExternalId, cancellationToken);
    }
}