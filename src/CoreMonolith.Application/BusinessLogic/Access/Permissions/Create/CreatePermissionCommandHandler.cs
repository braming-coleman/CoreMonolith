using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.Domain.Models.Access.Permissions;
using CoreMonolith.SharedKernel;

namespace CoreMonolith.Application.BusinessLogic.Access.Permissions.Create;

internal sealed class CreatePermissionCommandHandler(IUnitOfWork _unitOfWork)
    : ICommandHandler<CreatePermissionCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreatePermissionCommand command, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Access.PermissionRepository.ExistsByKeyAsync(command.Key, cancellationToken))
            return Result.Failure<Guid>(PermissionErrors.ExistsByKey);

        var permission = new Permission
        {
            Id = Guid.NewGuid(),
            Key = command.Key,
            Description = command.Description
        };

        permission.Raise(new PermissionCreatedDomainEvent(permission.Id));

        await _unitOfWork
            .Access
            .PermissionRepository
            .AddAsync(permission, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return permission.Id;
    }
}
