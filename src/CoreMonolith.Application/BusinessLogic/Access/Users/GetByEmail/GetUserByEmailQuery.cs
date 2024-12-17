using CoreMonolith.Application.Abstractions.Messaging;
using CoreMonolith.Application.BusinessLogic.Access.Users;

namespace CoreMonolith.Application.BusinessLogic.Access.Users.GetByEmail;

public sealed record GetUserByEmailQuery(string Email) : IQuery<UserResponse>;
