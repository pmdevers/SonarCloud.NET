using System.Text.Json.Serialization;

namespace SonarCloud.NET.Models;

public class Impact
{
    [JsonPropertyName("softwareQuality")]
    public string SoftwareQuality { get; set; } = string.Empty;
    [JsonPropertyName("severity")]
    public string Severity { get; set; } = string.Empty;
}
