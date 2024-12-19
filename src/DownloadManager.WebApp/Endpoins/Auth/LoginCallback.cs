﻿using CoreMonolith.SharedKernel.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace DownloadManager.WebApp.Endpoins.Auth;

internal sealed class LoginCallback : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/authentication/login-callback", async (HttpContext context, CancellationToken cancellation) =>
        {
            // This action handles the Keycloak callback after login
            var authenticateResult = await context.AuthenticateAsync(OpenIdConnectDefaults.AuthenticationScheme);
            if (authenticateResult.Succeeded)
                Results.LocalRedirect("/");
            else
                Results.BadRequest("Authentication failed.");
        });

        app.MapPost("/authentication/login-callback", async (HttpContext context) =>
        {
            // This action handles the Keycloak callback after login
            var authenticateResult = await context.AuthenticateAsync(OpenIdConnectDefaults.AuthenticationScheme);

            if (authenticateResult.Succeeded)
                Results.LocalRedirect("/");
            else
                Results.BadRequest("Authentication failed.");
        });
    }
}


//app.MapGet("/authentication/login-callback", async(HttpContext context) =>
//{
//    // This action handles the Keycloak callback after login
//    var authenticateResult = await context.AuthenticateAsync(OpenIdConnectDefaults.AuthenticationScheme);

//    if (authenticateResult.Succeeded)
//        Results.LocalRedirect("/");
//    else
//        Results.BadRequest("Authentication failed.");
//});

//app.MapPost("/authentication/login-callback", async (HttpContext context) =>
//{
//    // This action handles the Keycloak callback after login
//    var authenticateResult = await context.AuthenticateAsync(OpenIdConnectDefaults.AuthenticationScheme);

//    if (authenticateResult.Succeeded)
//        Results.LocalRedirect("/");
//    else
//        Results.BadRequest("Authentication failed.");
//});

//app.MapPost("/signin-oidc", async (HttpContext context) =>
//{
//    // This action handles the Keycloak callback after login
//    var authenticateResult = await context.AuthenticateAsync(OpenIdConnectDefaults.AuthenticationScheme);

//    if (authenticateResult.Succeeded)
//        Results.LocalRedirect("/");
//    else
//        Results.BadRequest("Authentication failed.");
//});

//app.MapGet("/signin-oidc", async (HttpContext context) =>
//{
//    // This action handles the Keycloak callback after login
//    var authenticateResult = await context.AuthenticateAsync(OpenIdConnectDefaults.AuthenticationScheme);

//    if (authenticateResult.Succeeded)
//        Results.LocalRedirect("/");
//    else
//        Results.BadRequest("Authentication failed.");
//});