using System.Text.Json.Serialization;

namespace SonarCloud.NET.Models;

public class Webhook
{
    [JsonPropertyName("key")]
    public required string Key { get; set; }
    
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("url")]
    public required Uri Url { get; set; }

    [JsonPropertyName("hasSecret")]
    public bool HasSecret { get; set; }
}
