using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.Domain.Access;
using CoreMonolith.SharedKernel;

namespace CoreMonolith.Application.Access.UserPermissions.Create;

internal sealed class CreateUserPermissionCommandHandler(IUnitOfWork _unitOfWork)
    : ICommandHandler<CreateUserPermissionCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateUserPermissionCommand command, CancellationToken cancellationToken)
    {
        //check user existance
        if (!await _unitOfWork.Access.UserRepository.ExistsByIdAsync(command.UserId, cancellationToken))
            return Result.Failure<Guid>(UserErrors.NotFound(command.UserId));

        //check permission existance 
        if (!await _unitOfWork.Access.PermissionRepository.ExistsByIdAsync(command.PermissionId, cancellationToken))
            return Result.Failure<Guid>(PermisionErrors.NotFound(command.PermissionId));

        //check user permission existance
        if (await _unitOfWork.Access.UserPermissionRepository
            .ExistsByUserAndPermissionIdAsync(command.UserId, command.PermissionId, cancellationToken))
            return Result.Failure<Guid>(UserPermisionErrors.ExistsByUserAndPermissionId);

        var userPermission = new UserPermission
        {
            Id = Guid.NewGuid(),
            UserId = command.UserId,
            PermissionId = command.PermissionId,
        };

        //raise domain event event
        userPermission.Raise(new UserPermissionCreatedDomainEvent(userPermission.Id));

        //save user permission
        await _unitOfWork.Access.UserPermissionRepository.AddAsync(userPermission, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        //return userpermission id
        return userPermission.Id;
    }
}
