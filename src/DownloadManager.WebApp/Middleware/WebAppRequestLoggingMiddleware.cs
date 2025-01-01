using Microsoft.Extensions.Primitives;
using Serilog.Context;

namespace DownloadManager.WebApp.Middleware;

public class WebAppRequestLoggingMiddleware(
    RequestDelegate _next,
    ILogger<WebAppRequestLoggingMiddleware> _logger)
{
    private const string CorrelationIdHeaderName = "Correlation-Id";

    public async Task Invoke(HttpContext context)
    {
        using (LogContext.PushProperty("CorrelationId", GetCorrelationId(context)))
        using (LogContext.PushProperty("Request.QueryString", context.Request.QueryString))
        using (LogContext.PushProperty("Request.RouteValues", context.Request.RouteValues))
        {
            _logger.LogInformation("Web app request started.");

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
