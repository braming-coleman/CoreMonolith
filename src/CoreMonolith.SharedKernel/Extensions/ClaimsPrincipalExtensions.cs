using System.Security.Claims;

namespace CoreMonolith.SharedKernel.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal? principal)
    {
        string? userId = principal?.FindFirstValue("userid");

        return Guid.TryParse(userId, out Guid parsedUserId) ?
            parsedUserId :
            throw new ArgumentNullException(nameof(principal), "User id is unavailable");
    }
}
