//using CoreMonolith.Infrastructure.Clients.HttpClients.Access;
//using CoreMonolith.SharedKernel.Abstractions;
//using CoreMonolith.SharedKernel.Constants;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.OpenIdConnect;
//using System.Security.Claims;

//namespace DownloadManager.WebApp.Endpoins.Auth;

//internal sealed class LoginCallback : IEndpoint
//{
//    public void MapEndpoint(IEndpointRouteBuilder app)
//    {
//        app.MapMethods("/authentication/login-callback", [HttpMethod.Get.Method, HttpMethod.Post.Method], async (
//            HttpContext context,
//            AccessApiClient accessClient,
//            CancellationToken cancellationToken) =>
//            {
//                // This action handles the Keycloak callback after login
//                var authenticateResult = await context.AuthenticateAsync(OpenIdConnectDefaults.AuthenticationScheme);
//                if (authenticateResult.Succeeded)
//                {
//                    var externalId = context.User.FindFirstValue(CustomClaimNames.NameIdentifier);
//                    if (externalId is null)
//                        Results.BadRequest("ExternalId not found.");
//                    else
//                    {
//                        var callbackResult = await accessClient.AuthCallbackAsync(
//                            new AuthCallbackRequest(
//                                new Guid(externalId),
//                                context.User.FindFirstValue(CustomClaimNames.PreferredUsername)!,
//                                context.User.FindFirstValue(CustomClaimNames.Givenname)!,
//                                context.User.FindFirstValue(CustomClaimNames.Surname)!,
//                                //TODO: Just for testing
//                                true),
//                            null,
//                            cancellationToken);

//                        Results.LocalRedirect("/");
//                    }
//                }
//                else
//                    Results.BadRequest("Authentication failed.");
//            })
//            .RequireAuthorization();
//    }
//}