using System.Net.Http.Json;
using System.Text.Json;

namespace Taller.Desktop.Services;

public sealed class AuthApiClient
{
    private readonly HttpClient _http = new()
    {
        BaseAddress = new Uri("https://localhost:5001")
    };

    public async Task<string?> LoginAsync(string username, string password)
    {
        var response = await _http.PostAsJsonAsync("/api/auth/login", new { username, password });
        if (!response.IsSuccessStatusCode) return null;

        using var stream = await response.Content.ReadAsStreamAsync();
        using var doc = await JsonDocument.ParseAsync(stream);
        return doc.RootElement.TryGetProperty("access_token", out var token) ? token.GetString() : null;
    }
}
