using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.SharedKernel.ValueObjects;
using Modules.UserService.Domain.Abstractions.Repositories;
using Modules.UserService.Domain.Models.Users;

namespace Modules.UserService.Application.BusinessLogic.Users.GetById;

public sealed record GetUserByIdQuery(Guid UserId) : IQuery<UserResult>;

internal sealed class GetUserByIdQueryHandler(
    IUserRepository _userRepo,
    IUserPermissionGroupRepository _userGroupRepo)
    : IQueryHandler<GetUserByIdQuery, UserResult>
{
    public async Task<Result<UserResult>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var user = await _userRepo.GetByIdAsync(query.UserId, cancellationToken);

        if (user is null)
            return Result.Failure<UserResult>(UserErrors.NotFound(query.UserId));

        var permissions = await _userGroupRepo.GetPermissionsByUserId(user.Id, cancellationToken);

        return new UserResult(user, permissions);
    }
}