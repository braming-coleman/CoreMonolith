using CoreMonolith.SharedKernel.Constants;
using System.Security.Claims;

namespace CoreMonolith.SharedKernel.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal? principal)
    {
        string? userId = principal?.FindFirstValue(CustomClaimNames.NameIdentifier);

        return Guid.TryParse(userId, out Guid parsedUserId) ?
            parsedUserId :
            throw new ArgumentNullException(nameof(principal), "Sub is unavailable");
    }
}
