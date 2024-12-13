using CoreMonolith.Domain.Access;

namespace CoreMonolith.Application.Abstractions.Authentication;

public interface ITokenProvider
{
    string Create(User user);
}
