using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.Domain.Models.Access.Users;
using CoreMonolith.SharedKernel.ValueObjects;

namespace CoreMonolith.Application.BusinessLogic.Access.Users.Register;

internal sealed class RegisterUserCommandHandler(
    IUnitOfWork _unitOfWork)
    : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Access.UserRepository.ExistsByEmailAsync(command.Email, cancellationToken))
            return Result.Failure<Guid>(UserErrors.EmailNotUnique);

        var user = new User
        {
            Id = Guid.CreateVersion7(),
            Email = command.Email,
            FirstName = command.FirstName,
            LastName = command.LastName
        };

        user.Raise(new UserRegisteredDomainEvent(user.Id));

        await _unitOfWork.Access.UserRepository.AddAsync(user, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
