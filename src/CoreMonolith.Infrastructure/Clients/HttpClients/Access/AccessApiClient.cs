using CoreMonolith.Application.BusinessLogic.Access.Users.AuthCallback;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace CoreMonolith.Infrastructure.Clients.HttpClients.Access;

public class AccessApiClient(HttpClient _httpClient)
{
    public async Task<ProcessKeycloakAuthCallbackResult?> AuthCallbackAsync(AuthCallbackRequest request, string token, CancellationToken cancellationToken = default)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PostAsJsonAsync("/core-api/v1/access/user/auth-callback",
            request,
            cancellationToken);

        ProcessKeycloakAuthCallbackResult? result = null;

        if (response is not null && response.IsSuccessStatusCode)
            result = await response.Content.ReadFromJsonAsync<ProcessKeycloakAuthCallbackResult>(cancellationToken);

        return result;
    }
}
