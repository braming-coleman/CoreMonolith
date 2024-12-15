using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.Domain.Access.Users;
using CoreMonolith.SharedKernel;

namespace CoreMonolith.Application.Access.Users.GetByEmail;

internal sealed class GetUserByEmailQueryHandler(
    IUnitOfWork _unitOfWork)
    : IQueryHandler<GetUserByEmailQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Access.UserRepository.GetByEmailAsync(query.Email, cancellationToken);

        if (user is null)
            return Result.Failure<UserResponse>(UserErrors.NotFoundByEmail);

        var userResult = new UserResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        };

        return userResult;
    }
}
