using CoreMonolith.SharedKernel.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace CoreMonolith.Application.Behaviors;

internal sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>(
    ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> _logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : Result
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;

        _logger.LogInformation("Processing request {RequestName}", requestName);

        TResponse result = await next();

        if (result.IsSuccess)
            _logger.LogInformation("Completed request {RequestName}", requestName);
        else
            using (LogContext.PushProperty("Error", result.Error, true))
                _logger.LogError("Completed request {RequestName} with error", requestName);

        return result;
    }
}
