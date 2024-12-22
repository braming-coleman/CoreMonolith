using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.SharedKernel.ValueObjects;

namespace CoreMonolith.Application.BusinessLogic.UserPermissionGroups.GetPermissionsByUserId;

internal sealed class GetPermissionsByUserIdQueryHandler(IUnitOfWork _unitOfWork)
    : IQueryHandler<GetPermissionsByUserIdQuery, HashSet<string>>
{
    public async Task<Result<HashSet<string>>> Handle(GetPermissionsByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Access
            .UserPermissionGroupRepository.GetPermissionsByUserId(request.UserId, cancellationToken);
    }
}
