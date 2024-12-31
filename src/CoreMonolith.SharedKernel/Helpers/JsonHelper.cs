using System.Text.Json;

namespace CoreMonolith.SharedKernel.Helpers;

public static partial class JsonHelper
{
    public static Task<string> SerializeAsync<T>(T value)
    {
        var result = JsonSerializer.Serialize(value, JsonSerializerOptions.Web);

        return Task.FromResult(result);
    }

    public static Task<T?> DeserializeAsync<T>(string value)
    {
        var result = JsonSerializer.Deserialize<T>(value, JsonSerializerOptions.Web);

        return Task.FromResult(result);
    }
}
