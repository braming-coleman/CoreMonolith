using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CoreMonolith.Api.Swagger;

public class CustomSecurityRequirementsOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
        operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });
        operation.Responses.Add("404", new OpenApiResponse { Description = "Not Found" });

        var bearerAuthScheme = new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
        };

        operation.Security =
        [
            new OpenApiSecurityRequirement
            {
                [ bearerAuthScheme ] = ["core-api-access"]
            }
        ];
    }
}
