using System.Text.Json.Serialization;

namespace SonarCloud.NET.Models;
public class Component
{
    [JsonPropertyName("organization")]
    public string Organization { get; set; } = string.Empty;
    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;
    [JsonPropertyName("qualifier")]
    public string Qualifier { get; set; } = string.Empty;
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("project")]
    public string Project { get; set; } = string.Empty;
}
