using CoreMonolith.Application.Abstractions.Authentication;
using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.Domain.Access;
using CoreMonolith.SharedKernel;

namespace CoreMonolith.Application.Access.Users.Register;

internal sealed class RegisterUserCommandHandler(
    IUnitOfWork _unitOfWork,
    IPasswordHasher _passwordHasher)
    : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Access.UserRepository.ExistsByEmailAsync(command.Email, cancellationToken))
            return Result.Failure<Guid>(UserErrors.EmailNotUnique);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = command.Email,
            FirstName = command.FirstName,
            LastName = command.LastName,
            PasswordHash = _passwordHasher.Hash(command.Password)
        };

        user.Raise(new UserRegisteredDomainEvent(user.Id));

        await _unitOfWork.Access.UserRepository.AddAsync(user, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
