using CoreMonolith.Application.Abstractions.Messaging;

namespace CoreMonolith.Application.Users.GetByEmail;

public sealed record GetUserByEmailQuery(string Email) : IQuery<UserResponse>;
