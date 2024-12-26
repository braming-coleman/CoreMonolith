using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.SharedKernel.ValueObjects;
using Modules.UserService.Domain.Abstractions.Repositories;
using Modules.UserService.Domain.Models.Users;

namespace Modules.UserService.Application.BusinessLogic.Users.GetByEmail;

public sealed record GetUserByEmailQuery(string Email) : IQuery<UserResult>;

internal sealed class GetUserByEmailQueryHandler(
    IUserRepository _userRepo,
    IUserPermissionGroupRepository _userGroupRepo)
    : IQueryHandler<GetUserByEmailQuery, UserResult>
{
    public async Task<Result<UserResult>> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken)
    {
        var user = await _userRepo.GetByEmailAsync(query.Email, cancellationToken);

        if (user is null)
            return Result.Failure<UserResult>(UserErrors.NotFoundByEmail(query.Email));

        var permissions = await _userGroupRepo.GetPermissionsByUserId(user.Id, cancellationToken);

        return new UserResult(user, permissions);
    }
}
