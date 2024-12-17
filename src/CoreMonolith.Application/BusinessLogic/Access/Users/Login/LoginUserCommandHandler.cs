using CoreMonolith.Application.Abstractions.Authentication;
using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.Domain.Models.Access.Users;
using CoreMonolith.SharedKernel;

namespace CoreMonolith.Application.BusinessLogic.Access.Users.Login;

internal sealed class LoginUserCommandHandler(
    IUnitOfWork _unitOfWork,
    IPasswordHasher _passwordHasher,
    ITokenProvider _tokenProvider) : ICommandHandler<LoginUserCommand, string>
{
    public async Task<Result<string>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Access.UserRepository.GetByEmailAsync(command.Email, cancellationToken);

        if (user is null)
            return Result.Failure<string>(UserErrors.NotFoundByEmail);

        bool verified = _passwordHasher.Verify(command.Password, user.PasswordHash);

        if (!verified)
            return Result.Failure<string>(UserErrors.NotFoundByEmail);

        string token = _tokenProvider.Create(user);

        return token;
    }
}
