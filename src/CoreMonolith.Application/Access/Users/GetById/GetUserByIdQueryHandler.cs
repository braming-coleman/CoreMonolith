using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.Domain.Abstractions.Repositories;
using CoreMonolith.Domain.Access;
using CoreMonolith.SharedKernel;

namespace CoreMonolith.Application.Access.Users.GetById;

internal sealed class GetUserByIdQueryHandler(
    IUnitOfWork _unitOfWork)
    : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Access.UserRepository.GetByIdAsync(query.UserId, cancellationToken);

        if (user is null)
            return Result.Failure<UserResponse>(UserErrors.NotFound(query.UserId));

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
