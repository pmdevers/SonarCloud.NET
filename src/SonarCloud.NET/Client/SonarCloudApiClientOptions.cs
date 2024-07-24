using System.Text.Json;

namespace SonarCloud.NET.Client;

public class SonarCloudApiClientOptions
{
    private const string SonarEndpoint = "https://sonarcloud.io/";

    private SonarCloudApiClientOptions() { }

    public static SonarCloudApiClientOptions Default => new();

    public JsonSerializerOptions JsonOptions { get; set; } = JsonSerializerOptions.Default;
    public string AccessToken { get; set; } = string.Empty;
    public Uri BaseAddress { get; set; } = new Uri(SonarEndpoint);
}
