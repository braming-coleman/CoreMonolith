using CoreMonolith.SharedKernel.Constants;
using System.Security.Claims;

namespace CoreMonolith.SharedKernel.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal? principal)
    {
        string? userId = principal?.FindFirstValue(CustomClaimNames.InternalUserId);

        return Guid.TryParse(userId, out Guid parsedUserId) ?
            parsedUserId :
            throw new ArgumentNullException(nameof(principal), $"{CustomClaimNames.InternalUserId} is unavailable");
    }

    public static Guid GetExternalUserId(this ClaimsPrincipal? principal)
    {
        string? userId = principal?.FindFirstValue(CustomClaimNames.NameIdentifier);

        return Guid.TryParse(userId, out Guid parsedUserId) ?
            parsedUserId :
            throw new ArgumentNullException(nameof(principal), $"{CustomClaimNames.NameIdentifier} is unavailable");
    }
}
