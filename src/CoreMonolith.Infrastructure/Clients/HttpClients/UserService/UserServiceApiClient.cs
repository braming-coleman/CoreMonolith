using CoreMonolith.SharedKernel.Constants;
using Modules.UserService.Api.ResponseModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace CoreMonolith.Infrastructure.Clients.HttpClients.UserService;

public class UserServiceApiClient(HttpClient _httpClient)
{
    public async Task<UserResponse?> AuthCallbackAsync(
        AuthCallbackRequest request,
        string token, CancellationToken
        cancellationToken = default)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PostAsJsonAsync("/core-api/v1/user-service/auth-callback",
            request,
            cancellationToken);

        UserResponse? result = null;

        if (response is not null && response.IsSuccessStatusCode)
            result = await response.Content.ReadFromJsonAsync<UserResponse>(cancellationToken);

        return result;
    }

    public async Task<Guid?> PermissionCreateAsync(
        CreatePermissionRequest request,
        CancellationToken cancellationToken = default)
    {
        _httpClient.DefaultRequestHeaders.Add(
            EndpointConstants.IdempotencyHeaderKeyName,
            Guid.CreateVersion7().ToString());

        var response = await _httpClient.PostAsJsonAsync("/core-api/v1/user-service/permission/create",
            request,
            cancellationToken);

        Guid? result = null;

        if (response is not null && response.IsSuccessStatusCode)
            result = await response.Content.ReadFromJsonAsync<Guid>(cancellationToken);

        return result;
    }
}
