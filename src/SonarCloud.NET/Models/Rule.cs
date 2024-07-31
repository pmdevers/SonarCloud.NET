using System.Text.Json.Serialization;

namespace SonarCloud.NET.Models;

public class Rule
{
    [JsonPropertyName("key")]
    public required string Key { get; set; }
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("securityCategory")]
    public required string SecurityCategory { get; set; }
    [JsonPropertyName("vulnerabilityProbability")]
    public required string VulnerabilityProbability { get; set; }
}
