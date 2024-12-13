using Microsoft.AspNetCore.Builder;
using Serilog;

namespace CoreMonolith.SharedKernel.Extensions
{
    public static class LoggingExtensions
    {
        public static WebApplicationBuilder AddAndConfigureSerilog(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((hostContext, configuration) =>
            {
                configuration.WriteTo.Console();
                configuration.WriteTo.OpenTelemetry();
            });

            return builder;
        }
    }
}
