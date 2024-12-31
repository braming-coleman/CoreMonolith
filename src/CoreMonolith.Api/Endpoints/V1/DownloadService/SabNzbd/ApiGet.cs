﻿using CoreMonolith.Api.Swagger.Examples.SabNzbd;
using CoreMonolith.SharedKernel.Abstractions;
using CoreMonolith.SharedKernel.Constants;
using CoreMonolith.SharedKernel.Extensions;
using Microsoft.AspNetCore.Mvc;
using Modules.DownloadService.Api.Usenet.SabNzbd;
using Modules.DownloadService.Api.Usenet.SabNzbd.Models.Api;
using Swashbuckle.AspNetCore.Filters;

namespace CoreMonolith.Api.Endpoints.V1.DownloadService.SabNzbd;

public class ApiGet : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {


        app
            .MapApiVersion("download-service", Versions.V1)
            .MapGet("/intercept/sabnzbd/api",
        [SwaggerResponseExample(200, typeof(CustomResponseExamples))]
        async ([FromQuery(Name = "output")] string output,
                [FromQuery(Name = "apikey")] string apiKey,
                [FromQuery(Name = "mode")] string mode,
                ISabNzbdServiceApi _serviceApi,
                CancellationToken cancellationToken) =>
            {
                var request = new GetRequest
                {
                    ApiKey = apiKey,
                    Mode = mode,
                    Output = output
                };

                return await _serviceApi.HandGetRequestAsync(request, cancellationToken);
            })
            .AllowAnonymous()
            .DisableAntiforgery()
            .Produces<VersionResponse>()
            .WithTags(Tags.DownloadService);
    }
}