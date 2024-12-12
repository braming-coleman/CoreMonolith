using CoreMonolith.Application.Abstractions.Messaging;

namespace CoreMonolith.Application.Access.Users.GetById;

public sealed record GetUserByIdQuery(Guid UserId) : IQuery<UserResponse>;
