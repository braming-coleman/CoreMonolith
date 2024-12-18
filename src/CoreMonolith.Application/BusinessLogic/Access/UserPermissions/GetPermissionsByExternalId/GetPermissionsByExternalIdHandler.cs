using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.SharedKernel;

namespace CoreMonolith.Application.BusinessLogic.Access.UserPermissions.GetPermissionsByExternalId;

internal sealed class GetPermissionsByExternalIdHandler(
    IUnitOfWork _unitOfWork)
    : IQueryHandler<GetPermissionsByExternalIdQuery, HashSet<string>>
{
    public async Task<Result<HashSet<string>>> Handle(GetPermissionsByExternalIdQuery request, CancellationToken cancellationToken)
    {
        var permissions = await _unitOfWork
            .Access
            .UserPermissionRepository
            .GetPermissionsByExternalIdAsync(request.ExternalId, cancellationToken);

        return permissions;
    }
}
