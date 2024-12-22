using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.Domain.Models.Access.PermissionGroups;
using CoreMonolith.Domain.Models.Access.UserPermissionGroups;
using CoreMonolith.Domain.Models.Access.Users;
using CoreMonolith.SharedKernel.Constants;
using CoreMonolith.SharedKernel.ValueObjects;

namespace CoreMonolith.Application.BusinessLogic.Access.Users.AuthCallback;

internal sealed class ProcessKeycloakAuthCallbackCommandHandler(IUnitOfWork _unitOfWork)
    : ICommandHandler<ProcessKeycloakAuthCallbackCommand, ProcessKeycloakAuthCallbackResult>
{
    public async Task<Result<ProcessKeycloakAuthCallbackResult>> Handle(ProcessKeycloakAuthCallbackCommand request, CancellationToken cancellationToken)
    {
        var result = await EnsureUserAsync(request, cancellationToken);
        if (!result.IsSuccess)
            return result;

        //retreive permissions
        var permissions = await _unitOfWork.Access
            .UserPermissionGroupRepository.GetPermissionsByUserId(result.Value.UserId, cancellationToken);

        return new ProcessKeycloakAuthCallbackResult(result.Value.UserId, permissions);
    }

    private async Task<Result<ProcessKeycloakAuthCallbackResult>> EnsureUserAsync(ProcessKeycloakAuthCallbackCommand request, CancellationToken cancellationToken)
    {
        var dbUser = await _unitOfWork.Access.UserRepository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        dbUser ??= await _unitOfWork.Access.UserRepository.GetByEmailAsync(request.Email, cancellationToken);

        var changed = false;
        if (dbUser is null)
        {
            dbUser = new User
            {
                Id = Guid.CreateVersion7(),
                ExternalId = request.ExternalId,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            dbUser.Raise(new UserRegisteredDomainEvent(dbUser.Id));

            await _unitOfWork.Access.UserRepository.AddAsync(dbUser, cancellationToken);

            var result = await AddPermissions(request, dbUser, cancellationToken);
            if (!result.IsSuccess)
                return result;

            changed = true;
        }
        //update user based on recevied data if it has changed.
        else if (!Equals(dbUser, request))
        {
            dbUser.ExternalId = request.ExternalId;
            dbUser.Email = request.Email;
            dbUser.FirstName = request.FirstName;
            dbUser.LastName = request.LastName;

            dbUser.Raise(new UserUpdatedDomainEvent(dbUser.Id));

            _unitOfWork.Access.UserRepository.Update(dbUser);

            changed = true;
        }

        if (changed)
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ProcessKeycloakAuthCallbackResult(dbUser.Id, []);
    }

    private async Task<Result<ProcessKeycloakAuthCallbackResult>> AddPermissions(
        ProcessKeycloakAuthCallbackCommand request,
        User dbUser,
        CancellationToken cancellationToken)
    {
        //add permissions based on admin flag
        //get admin group or get user group
        PermissionGroup? group;
        if (request.AdminUser)
            group = await _unitOfWork.Access.PermissionGroupRepository.FindByCodeAsync(ApiPermissionGroups.Admin, cancellationToken);
        else
            group = await _unitOfWork.Access.PermissionGroupRepository.FindByCodeAsync(ApiPermissionGroups.User, cancellationToken);

        var type = request.AdminUser ? ApiPermissionGroups.Admin : ApiPermissionGroups.User;
        if (group is null)
            return Result.Failure<ProcessKeycloakAuthCallbackResult>(PermissionGroupErrors.NotFound(type));

        //create new userpermissiongroup link
        var userPermissionGroup = new UserPermissionGroup
        {
            Id = Guid.CreateVersion7(),
            UserId = dbUser.Id,
            PermissionGroupId = group.Id
        };

        userPermissionGroup.Raise(new UserPermissionGroupChangedDomainEvent(type));

        await _unitOfWork.Access.UserPermissionGroupRepository.AddAsync(userPermissionGroup, cancellationToken);

        return new ProcessKeycloakAuthCallbackResult(dbUser.Id, []);
    }

    private static bool Equals(User user, ProcessKeycloakAuthCallbackCommand request) =>
        $"{user.ExternalId}|{user.Email}|{user.FirstName}|{user.LastName}"
        == $"{request.ExternalId}|{request.Email}|{request.FirstName}|{request.LastName}";
}
