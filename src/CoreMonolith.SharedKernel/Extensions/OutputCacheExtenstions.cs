using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CoreMonolith.SharedKernel.Extensions;

public static class OutputCacheExtenstions
{
    private static readonly TimeSpan DefaultExpiry = TimeSpan.FromMinutes(10);

    public static RouteHandlerBuilder CacheAuthedOutput(this RouteHandlerBuilder builder, string tag, TimeSpan? expireTime = null)
    {
        if (expireTime is null)
            expireTime = DefaultExpiry;

        builder.CacheOutput(options =>
            {
                options.Expire(expireTime.Value);
                options.Tag(tag);
                options.VaryByValue((httpContext, _) =>
                {
                    return ValueTask.FromResult(new KeyValuePair<string, string>(
                        "userid",
                        httpContext.User.GetUserId().ToString()));
                });
            }, true);

        return builder;
    }

    public static RouteHandlerBuilder CacheNonAuthedOutput(this RouteHandlerBuilder builder, string tag, TimeSpan? expireTime = null)
    {
        if (expireTime is null)
            expireTime = DefaultExpiry;

        builder.CacheOutput(options =>
        {
            options.Expire(expireTime.Value);
            options.Tag(tag);
        }, true);

        return builder;
    }
}
