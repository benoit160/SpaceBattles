namespace SpaceBattles.Core.Application.Services;

using System.Text.Json;
using System.Text.Json.Serialization;

public sealed class LoadService
{
    private const string ManifestFile = "service-worker-assets.js";

    private readonly HttpClient _httpClient;

    public LoadService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Manifest?> GetManifest()
    {
        string response = await _httpClient.GetStringAsync(ManifestFile);

        int first, last;

        first = response.IndexOf('{');
        last = response.LastIndexOf('}');
        int length = response.Length - first - (response.Length - last);

        ReadOnlyMemory<char> subset = response.AsMemory().Slice(first, length + 1);

        return JsonSerializer.Deserialize<Manifest>(subset.Span);
    }

    public record struct Asset(
        [property:JsonPropertyName("hash")]string Hash,
        [property:JsonPropertyName("url")]string Url);

    public class Manifest
    {
        [JsonPropertyName("assets")]
        public required Asset[] Assets { get; set; }
    }
}