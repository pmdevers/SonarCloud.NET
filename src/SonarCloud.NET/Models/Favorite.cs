using System.Text.Json.Serialization;

namespace SonarCloud.NET.Models;
public class Favorite
{
    [JsonPropertyName("organization")]
    public required  string Organization { get; set; }
    [JsonPropertyName("key")]
    public required string Key { get; set; }
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("qualifier")]
    public required string Qualifier { get; set; }
}
