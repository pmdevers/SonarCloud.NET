using System.Text.Json.Serialization;

namespace SonarCloud.NET.Models;

public class SourceFile
{
    [JsonPropertyName("key")]
    public required string Key { get; set; }
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("projectName")]
    public required string ProjectName { get; set; }
}
