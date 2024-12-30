using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.SharedKernel.Constants;
using CoreMonolith.SharedKernel.ValueObjects;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Logging;
using Modules.UserService.Domain.Abstractions.Repositories;
using Modules.UserService.Domain.Models.PermissionGroups;
using Modules.UserService.Domain.Models.UserPermissionGroups;
using Modules.UserService.Domain.Models.Users;

namespace Modules.UserService.Application.BusinessLogic.Users.AuthCallback;

public sealed record ProcessKeycloakAuthCallbackCommand(
    Guid ExternalId,
    string Email,
    string FirstName,
    string LastName,
    bool AdminUser = false)
    : ICommand<UserResult>;

internal class ProcessKeycloakAuthCallbackCommandValidator : AbstractValidator<ProcessKeycloakAuthCallbackCommand>
{
    public ProcessKeycloakAuthCallbackCommandValidator()
    {
        RuleFor(x => x.ExternalId).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress().Must(email => Email.Create(email).IsSuccess);
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
    }
}

public sealed record ProcessKeycloakAuthCallbackResult(
    User User,
    HashSet<string> Permissions);

internal sealed class ProcessKeycloakAuthCallbackCommandHandler(
    IUserRepository _userRepo,
    IPermissionGroupRepository _groupRepo,
    IUserPermissionGroupRepository _userGroupRepo,
    IUserServiceUow _unitOfWork)
    : ICommandHandler<ProcessKeycloakAuthCallbackCommand, UserResult>
{
    public async Task<Result<UserResult>> Handle(ProcessKeycloakAuthCallbackCommand request, CancellationToken cancellationToken)
    {
        var result = await EnsureUserAsync(request, cancellationToken);

        if (result.IsFailure)
            return result;

        if (result.Value.User is null)
            return Result.Failure<UserResult>(UserErrors.CreationFailed);

        //retreive permissions
        var permissions = await _userGroupRepo.GetPermissionsByUserId(result.Value.User.Id, cancellationToken);

        return new UserResult(result.Value.User, permissions);
    }

    private async Task<Result<UserResult>> EnsureUserAsync(ProcessKeycloakAuthCallbackCommand request, CancellationToken cancellationToken)
    {
        var dbUser = await _userRepo.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        dbUser ??= await _userRepo.GetByEmailAsync(request.Email, cancellationToken);

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

            await _userRepo.AddAsync(dbUser, cancellationToken);

            var result = await AddPermissions(request, dbUser, cancellationToken);
            if (!result.IsSuccess)
                return result;

            changed = true;
        }
        //update user based on recevied data if it has changed.
        else if (DetailsChanged(dbUser, request))
        {
            dbUser.ExternalId = request.ExternalId;
            dbUser.Email = request.Email;
            dbUser.FirstName = request.FirstName;
            dbUser.LastName = request.LastName;

            dbUser.Raise(new UserUpdatedDomainEvent(dbUser.Id));

            _userRepo.Update(dbUser);

            changed = true;
        }

        if (changed)
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new UserResult(dbUser, []);
    }

    private async Task<Result<UserResult>> AddPermissions(
        ProcessKeycloakAuthCallbackCommand request,
        User dbUser,
        CancellationToken cancellationToken)
    {
        //add permissions based on admin flag
        //get admin group or get user group
        var type = request.AdminUser ? ApiPermissionGroups.Admin : ApiPermissionGroups.User;

        var group = await _groupRepo.FindByCodeAsync(type, cancellationToken);
        if (group is null)
            return Result.Failure<UserResult>(PermissionGroupErrors.NotFound(type));

        //create new userpermissiongroup link
        var userPermissionGroup = new UserPermissionGroup
        {
            Id = Guid.CreateVersion7(),
            UserId = dbUser.Id,
            PermissionGroupId = group.Id
        };

        userPermissionGroup.Raise(new UserPermissionGroupChangedDomainEvent(type));

        await _userGroupRepo.AddAsync(userPermissionGroup, cancellationToken);

        return new UserResult(dbUser, []);
    }

    private static bool DetailsChanged(User user, ProcessKeycloakAuthCallbackCommand request) =>
        $"{user.ExternalId}|{user.Email}|{user.FirstName}|{user.LastName}"
        != $"{request.ExternalId}|{request.Email}|{request.FirstName}|{request.LastName}";
}

internal sealed class UserPermissionGroupChangedDomainEventHandler(
    IOutputCacheStore _cacheStore,
    ILogger<UserPermissionGroupChangedDomainEventHandler> _logger)
    : INotificationHandler<UserPermissionGroupChangedDomainEvent>
{
    public async Task Handle(UserPermissionGroupChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogWarning("User permissions changed - {Notification}", notification);

        await _cacheStore.EvictByTagAsync(Tags.UserPermission, cancellationToken);
    }
}

internal sealed class UserRegisteredFromKeycloakDomainEventHandler(
    IOutputCacheStore _cacheStore,
    ILogger<UserRegisteredFromKeycloakDomainEventHandler> _logger)
    : INotificationHandler<UserRegisteredDomainEvent>
{
    public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogWarning("User registered - {Notification}", notification);

        await _cacheStore.EvictByTagAsync(Tags.User, cancellationToken);
    }
}

internal sealed class UserUpdatedFromKeycloakDomainEventHandler(
    IOutputCacheStore _cacheStore,
    ILogger<UserUpdatedFromKeycloakDomainEventHandler> _logger)
    : INotificationHandler<UserUpdatedDomainEvent>
{
    public async Task Handle(UserUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogWarning("User updated - {Notification}", notification);

        await _cacheStore.EvictByTagAsync(Tags.User, cancellationToken);
    }
}
