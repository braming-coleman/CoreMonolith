using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.SharedKernel.ValueObjects;

namespace CoreMonolith.Application.BusinessLogic.UserPermissionGroups.GetPermissionsByExternalId;

internal sealed class GetPermissionsByExternalIdQueryHandler(IUnitOfWork _unitOfWork)
    : IQueryHandler<GetPermissionsByExternalIdQuery, HashSet<string>>
{
    public async Task<Result<HashSet<string>>> Handle(GetPermissionsByExternalIdQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Access
            .UserPermissionGroupRepository.GetPermissionsByExternalId(request.ExternalId, cancellationToken);
    }
}
