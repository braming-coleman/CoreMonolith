using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Net.Http.Headers;

namespace CoreMonolith.Infrastructure.Clients.HttpClients;

internal sealed class KeycloakTokenHandler(
    IHttpContextAccessor _httpContextAccessor)
    : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _httpContextAccessor.HttpContext!.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

        if (!string.IsNullOrEmpty(token))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await base.SendAsync(request, cancellationToken);
    }
}
