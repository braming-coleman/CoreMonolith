﻿using Microsoft.OpenApi.Models;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models.Api;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CoreMonolith.Api.Swagger;

public class CustomSchemasDocFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        context.SchemaGenerator.GenerateSchema(typeof(ConfigResponse), context.SchemaRepository);
    }
}