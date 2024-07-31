using System.Text.Json.Serialization;

namespace SonarCloud.NET.Models;

public class User
{
    [JsonPropertyName("login")]
    public required string Login { get; set; }
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("active")]
    public required bool Active { get; set; }
}
