using CoreMonolith.Domain.Access.Users;

namespace CoreMonolith.Application.Abstractions.Authentication;

public interface ITokenProvider
{
    string Create(User user);
}
