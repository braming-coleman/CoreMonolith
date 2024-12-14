using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.SharedKernel;

namespace CoreMonolith.Application.Access.UserPermissions.GetPermissionsByUserId;

internal sealed class GetPermissionsByUserIdQueryHandler(
    IUnitOfWork _unitOfWork)
    : IQueryHandler<GetPermissionsByUserIdQuery, HashSet<string>>
{
    public async Task<Result<HashSet<string>>> Handle(GetPermissionsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var permissions = await _unitOfWork
            .Access
            .UserPermissionRepository
            .GetPermissionsByUserIdAsync(request.UserId);

        return permissions;
    }
}
