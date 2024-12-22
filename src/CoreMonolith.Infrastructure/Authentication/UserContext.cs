using CoreMonolith.Application.Abstractions.Authentication;
using CoreMonolith.SharedKernel.Extensions;
using Microsoft.AspNetCore.Http;

namespace CoreMonolith.Infrastructure.Authentication;

internal sealed class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public Guid UserId =>
        _httpContextAccessor
            .HttpContext?
            .User
            .GetExternalUserId() ??
        throw new ArgumentNullException(nameof(httpContextAccessor), "User context is unavailable");
}
