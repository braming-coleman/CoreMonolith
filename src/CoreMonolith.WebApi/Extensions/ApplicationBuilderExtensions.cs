using Scalar.AspNetCore;

namespace CoreMonolith.WebApi.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSwaggerWithUi(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }

    public static IApplicationBuilder UseSwaggerWithScalar(this WebApplication app)
    {
        app.UseSwagger(options =>
        {
            options.RouteTemplate = "openapi/{documentName}.json";
        });
        app.MapScalarApiReference();

        return app;
    }
}
