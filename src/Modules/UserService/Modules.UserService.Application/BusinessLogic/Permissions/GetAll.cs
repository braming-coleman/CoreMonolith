using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.SharedKernel.ValueObjects;
using Modules.UserService.Api.ResponseModels;
using Modules.UserService.Domain.Abstractions.Repositories;

namespace Modules.UserService.Application.BusinessLogic.Permissions.GetAll;

public sealed record GetAllPermissionsQuery() : IQuery<List<PermissionResponse>>;

internal sealed class GetAllPermissionsQueryHandler(IPermissionRepository _permRepo)
    : IQueryHandler<GetAllPermissionsQuery, List<PermissionResponse>>
{
    public async Task<Result<List<PermissionResponse>>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
    {
        var permissions = await _permRepo.GetAllAsync(cancellationToken);

        var result = permissions
            .Select(s => new PermissionResponse(
                s.Id,
                s.Key,
                s.Description))
            .ToList();

        return result;
    }
}