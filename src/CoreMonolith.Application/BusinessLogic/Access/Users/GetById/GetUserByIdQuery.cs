using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.Application.BusinessLogic.Access.Users;

namespace CoreMonolith.Application.BusinessLogic.Access.Users.GetById;

public sealed record GetUserByIdQuery(Guid UserId) : IQuery<UserResponse>;
