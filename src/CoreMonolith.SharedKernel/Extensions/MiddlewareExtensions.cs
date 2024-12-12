using CoreMonolith.SharedKernel.Middleware;
using Microsoft.AspNetCore.Builder;

namespace CoreMonolith.SharedKernel.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseRequestContextLogging(this IApplicationBuilder app)
    {
        app.UseMiddleware<RequestContextLoggingMiddleware>();

        return app;
    }
}
