using CoreMonolith.Application.Abstractions.Authentication;
using CoreMonolith.Application.Abstractions.Data;
using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.Domain.Users;
using CoreMonolith.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace CoreMonolith.Application.Users.Login;

internal sealed class LoginUserCommandHandler(
    IApplicationDbContext context,
    IPasswordHasher passwordHasher,
    ITokenProvider tokenProvider) : ICommandHandler<LoginUserCommand, string>
{
    public async Task<Result<string>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        User? user = await context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Email == command.Email, cancellationToken);

        if (user is null)
            return Result.Failure<string>(UserErrors.NotFoundByEmail);

        bool verified = passwordHasher.Verify(command.Password, user.PasswordHash);

        if (!verified)
            return Result.Failure<string>(UserErrors.NotFoundByEmail);

        string token = tokenProvider.Create(user);

        return token;
    }
}
