using CoreMonolith.Application.Abstractions.Messaging;

namespace CoreMonolith.Application.Access.Users.Register;

public sealed record RegisterUserCommand(string Email, string FirstName, string LastName, string Password)
    : ICommand<Guid>;
