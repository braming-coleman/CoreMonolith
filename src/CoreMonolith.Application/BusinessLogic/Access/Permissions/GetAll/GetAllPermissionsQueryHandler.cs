using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.Application.BusinessLogic.Access.Permissions;
using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.SharedKernel;

namespace CoreMonolith.Application.BusinessLogic.Access.Permissions.GetAll;

internal sealed class GetAllPermissionsQueryHandler(IUnitOfWork _unitOfWork)
    : IQueryHandler<GetAllPermissionsQuery, List<PermissionReposnse>>
{
    public async Task<Result<List<PermissionReposnse>>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
    {
        var permissions = await _unitOfWork.Access
            .PermissionRepository
            .GetAllAsync(cancellationToken);

        var result = permissions
            .Select(s => new PermissionReposnse
            {
                Id = s.Id,
                Key = s.Key,
                Description = s.Description
            })
            .ToList();

        return result;
    }
}
