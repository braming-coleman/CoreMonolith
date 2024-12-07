using CoreMonolith.Domain.Users;

namespace CoreMonolith.Application.Abstractions.Authentication;

public interface ITokenProvider
{
    string Create(User user);
}
