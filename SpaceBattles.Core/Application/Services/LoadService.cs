using SkiaSharp;

namespace SpaceBattles.Core.Application.Services;

using System.Net;
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

    public async Task GetImages()
    {
        Manifest manifest = await GetAndReadManifest()
                            ?? throw new JsonException("Could not deserialize manifest.json");

        IEnumerable<Asset> images = manifest.Assets.Where(asset => asset.Url.Contains("images", StringComparison.OrdinalIgnoreCase));

        foreach (Asset asset in images)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(asset.Url);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                MemoryStream content = new MemoryStream();

                await response.Content.CopyToAsync(content);
                ResizeWithSkiaSharp(content);
            }
        }
    }

    public static void ResizeWithSkiaSharp(Stream stream)
    {
        using SKData? skData = SKData.Create(stream);
        using SKCodec? codec = SKCodec.Create(skData);

        var supportedScale = codec
            .GetScaledDimensions((float)150 / codec.Info.Width);

        var nearest = new SKImageInfo(supportedScale.Width, supportedScale.Height);
        using var destinationImage = SKBitmap.Decode(codec, nearest);
        using var resizedImage = destinationImage.Resize(new SKImageInfo(150, 150), SKFilterQuality.High);

        var format = SKEncodedImageFormat.Avif;
        using var outputImage = SKImage.FromBitmap(resizedImage);
        using var data = outputImage.Encode(format, 90);
        using var outputStream = GetOutputStream("skiasharp");
        data.SaveTo(outputStream);

        outputStream.Close();
        stream.Close();
    }

    private static Stream GetOutputStream(string name)
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        return File.Open(path + $"output_{name}.avif", FileMode.OpenOrCreate);
    }

    public async Task<Manifest?> GetAndReadManifest()
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