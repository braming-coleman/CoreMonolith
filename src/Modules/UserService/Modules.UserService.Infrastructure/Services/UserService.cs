using CoreMonolith.SharedKernel.ValueObjects;
using Mapster;
using MediatR;
using Modules.UserService.Api;
using Modules.UserService.Api.RequestModels;
using Modules.UserService.Api.ResponseModels;
using Modules.UserService.Application.BusinessLogic.Permissions.Create;
using Modules.UserService.Application.BusinessLogic.Permissions.GetAll;
using Modules.UserService.Application.BusinessLogic.UserPermissionGroups.GetPermissionsByExternalId;
using Modules.UserService.Application.BusinessLogic.UserPermissionGroups.GetPermissionsByUserId;
using Modules.UserService.Application.BusinessLogic.Users.AuthCallback;
using Modules.UserService.Application.BusinessLogic.Users.GetByEmail;
using Modules.UserService.Application.BusinessLogic.Users.GetById;

namespace Modules.UserService.Infrastructure.Services;

internal sealed class UserServiceApi(ISender _sender) : IUserServiceApi
{
    public async Task<Result<UserResponse>> AuthenticationCallbackAsync(AuthenticationCallbackRequest request, CancellationToken cancellationToken = default)
    {
        var command = request.Adapt<ProcessKeycloakAuthCallbackCommand>();

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return Result.Failure<UserResponse>(result.Error);

        return new UserResponse(
            result.Value.User!.Id,
            result.Value.User.ExternalId,
            result.Value.User.Email,
            result.Value.Permissions);
    }

    public async Task<Result<Guid>> PermissionCreateAsync(Guid requestId, PermissionRequest request, CancellationToken cancellationToken = default)
    {
        var command = new CreatePermissionCommand(requestId, request.Key, request.Description);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return Result.Failure<Guid>(result.Error);

        return result.Value;
    }

    public async Task<Result<List<PermissionResponse>>> PermissionsGetAll(CancellationToken cancellationToken = default)
    {
        var query = new GetAllPermissionsQuery();

        var result = await _sender.Send(query, cancellationToken);

        return result;
    }

    public async Task<Result<HashSet<string>>> PermissionsGetByExternalIdAsync(Guid externalId, CancellationToken cancellationToken = default)
    {
        var query = new GetPermissionsByExternalIdQuery(externalId);

        var result = await _sender.Send(query, cancellationToken);

        return result;
    }

    public async Task<Result<HashSet<string>>> PermissionsGetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var query = new GetPermissionsByUserIdQuery(userId);

        var result = await _sender.Send(query, cancellationToken);

        return result;
    }

    public async Task<Result<UserResponse?>> UserGetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var query = new GetUserByEmailQuery(email);

        var result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
            return Result.Failure<UserResponse?>(result.Error);

        return new UserResponse(
            result.Value.User!.Id,
            result.Value.User.ExternalId,
            result.Value.User.Email,
            result.Value.Permissions);
    }

    public async Task<Result<UserResponse?>> UserGetByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var query = new GetUserByIdQuery(userId);

        var result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
            return Result.Failure<UserResponse?>(result.Error);

        return new UserResponse(
            result.Value.User!.Id,
            result.Value.User.ExternalId,
            result.Value.User.Email,
            result.Value.Permissions);
    }
}
