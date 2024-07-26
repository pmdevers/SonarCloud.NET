using System.Text.Json.Serialization;

namespace SonarCloud.NET.Models;

public class Project
{
    [JsonPropertyName("organization")]
    public string? Organization { get; set; }
    [JsonPropertyName("key")]
    public required string Key { get; set; }
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("qualifier")]
    public required string Qualifier { get; set; }
    [JsonPropertyName("visibility")]
    public string? Visibility { get; set; }
    [JsonPropertyName("lastAnalysisDate")]
    public string? LastAnalysisDate { get; set; }

    [JsonPropertyName("revision")]
    public string? Revision { get; set; }

    public static Project Empty => new()
    {
        Key = string.Empty,
        Name = string.Empty,
        Qualifier = string.Empty
    };
}




