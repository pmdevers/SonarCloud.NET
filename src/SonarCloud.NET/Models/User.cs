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

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("groups")]
    public string[]? Groups { get;set;}

    [JsonPropertyName("tokensCount")]
    public int? TokensCount { get; set; }

    [JsonPropertyName("local")]
    public bool Local { get;set;}

    [JsonPropertyName("externalIdentity")]
    public string? ExternalIdentity { get; set; }

    [JsonPropertyName("externalProvider")]
    public string? ExternalProvider { get; set; }

    [JsonPropertyName("avatar")]
    public string? Avatar { get; set; }
}
