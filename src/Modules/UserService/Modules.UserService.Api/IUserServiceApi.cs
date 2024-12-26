using CoreMonolith.SharedKernel.ValueObjects;
using Modules.UserService.Api.RequestModels;
using Modules.UserService.Api.ResponseModels;

namespace Modules.UserService.Api;

public interface IUserServiceApi
{
    Task<Result<HashSet<string>>> PermissionsGetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<Result<HashSet<string>>> PermissionsGetByExternalIdAsync(Guid externalId, CancellationToken cancellationToken = default);

    Task<Result<List<PermissionResponse>>> PermissionsGetAll(CancellationToken cancellationToken = default);

    Task<Result<Guid>> PermissionCreateAsync(Guid requestId, PermissionRequest request, CancellationToken cancellationToken = default);

    Task<Result<UserResponse?>> UserGetByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<Result<UserResponse?>> UserGetByIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<Result<UserResponse>> AuthenticationCallbackAsync(AuthenticationCallbackRequest request, CancellationToken cancellationToken = default);
}
