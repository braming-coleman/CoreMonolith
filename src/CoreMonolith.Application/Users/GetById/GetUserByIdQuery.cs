using CoreMonolith.Application.Abstractions.Messaging;

namespace CoreMonolith.Application.Users.GetById;

public sealed record GetUserByIdQuery(Guid UserId) : IQuery<UserResponse>;
