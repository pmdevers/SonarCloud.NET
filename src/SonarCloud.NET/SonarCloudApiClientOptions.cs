using SonarCloud.NET.Helpers;
using System.Text.Json;

namespace SonarCloud.NET;

public class SonarCloudApiClientOptions
{
    private const string SonarEndpoint = "https://sonarcloud.io/";

    private SonarCloudApiClientOptions() 
    {
        JsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        JsonOptions.Converters.Add(new JsonDateTimeParseConverter());
    }

    public static SonarCloudApiClientOptions Default => new();

    public JsonSerializerOptions JsonOptions { get; set; }
    public string AccessToken { get; set; } = string.Empty;
    public Uri BaseAddress { get; set; } = new Uri(SonarEndpoint);
}
