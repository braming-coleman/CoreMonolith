﻿using CoreMonolith.SharedKernel.Abstractions;
using CoreMonolith.SharedKernel.Constants;
using CoreMonolith.SharedKernel.Extensions;

namespace CoreMonolith.WebApi.Endpoints.V2.Weather;

internal sealed class Forecast : IEndpoint
{
    readonly string[] summaries =
    [
        "Freezing",
        "Bracing",
        "Chilly",
        "Cool",
        "Mild",
        "Warm",
        "Balmy",
        "Hot",
        "Sweltering",
        "Scorching"
    ];

    internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app
            .MapApiVersion("weather", Versions.V2)
            .MapGet("/forecast", () =>
            {
                var forecast = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    (
                        DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        Random.Shared.Next(-20, 55),
                        $"V2 - {summaries[Random.Shared.Next(summaries.Length)]}"
                    ))
                    .ToArray();
                return forecast;
            })
            .WithTags(Tags.Weather);
    }
}
