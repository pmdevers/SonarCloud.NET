using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
    public ProjectVisibility? Visibility { get; set; }
    [JsonPropertyName("lastAnalysisDate")]
    public DateTime? LastAnalysisDate { get; set; }

    [JsonPropertyName("revision")]
    public string? Revision { get; set; }
    public static Project Empty => new() {
        Key = string.Empty, 
        Name = string.Empty, 
        Qualifier = string.Empty
        };
}
