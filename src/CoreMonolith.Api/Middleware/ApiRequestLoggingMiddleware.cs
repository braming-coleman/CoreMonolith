using Microsoft.Extensions.Primitives;
using Serilog.Context;

namespace CoreMonolith.Api.Middleware;

public class ApiRequestLoggingMiddleware(
    RequestDelegate _next,
    ILogger<ApiRequestLoggingMiddleware> _logger)
{
    private const string CorrelationIdHeaderName = "Correlation-Id";

    public async Task Invoke(HttpContext context)
    {
        using (LogContext.PushProperty("CorrelationId", GetCorrelationId(context)))
        using (LogContext.PushProperty("Request.QueryString", context.Request.QueryString))
        using (LogContext.PushProperty("Request.RouteValues", context.Request.RouteValues))
        {
            _logger.LogInformation("Api request started.");

            await _next.Invoke(context);
        }
    }

    private static string GetCorrelationId(HttpContext context)
    {
        context.Request.Headers.TryGetValue(
            CorrelationIdHeaderName,
            out StringValues correlationId);

        return correlationId.FirstOrDefault() ?? context.TraceIdentifier;
    }
}
