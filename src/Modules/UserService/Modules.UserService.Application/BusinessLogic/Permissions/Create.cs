using CoreMonolith.Application.Abstractions.Idempotency;
using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.SharedKernel.Constants;
using CoreMonolith.SharedKernel.ValueObjects;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Logging;
using Modules.UserService.Domain.Abstractions.Repositories;
using Modules.UserService.Domain.Models.Permissions;

namespace Modules.UserService.Application.BusinessLogic.Permissions.Create;

public sealed record CreatePermissionCommand(
    Guid RequestId,
    string Key,
    string Description) : IdempotentCommand<Guid>(RequestId);

internal sealed class CreatePermissionCommandValidator : AbstractValidator<CreatePermissionCommand>
{
    public CreatePermissionCommandValidator()
    {
        RuleFor(c => c.Key).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
    }
}

internal sealed class CreatePermissionCommandHandler(
    IPermissionRepository _permRepo,
    IUnitOfWork _unitOfWork)
    : ICommandHandler<CreatePermissionCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreatePermissionCommand command, CancellationToken cancellationToken)
    {
        if (await _permRepo.ExistsByKeyAsync(command.Key, cancellationToken))
            return Result.Failure<Guid>(PermissionErrors.ExistsByKey);

        var permission = new Permission
        {
            Id = Guid.CreateVersion7(),
            Key = command.Key,
            Description = command.Description
        };

        permission.Raise(new PermissionCreatedDomainEvent(permission.Id));

        await _permRepo.AddAsync(permission, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return permission.Id;
    }
}

internal sealed class PermissionCreatedDomainEventHandler(
    IOutputCacheStore _cacheStore,
    ILogger<PermissionCreatedDomainEventHandler> _logger)
    : INotificationHandler<PermissionCreatedDomainEvent>
{
    public async Task Handle(PermissionCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogWarning("Permission created - {notification}", notification);

        await _cacheStore.EvictByTagAsync(Tags.Permission, cancellationToken);
    }
}