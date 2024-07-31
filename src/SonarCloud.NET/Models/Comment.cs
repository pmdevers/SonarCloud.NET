using System.Text.Json.Serialization;

namespace SonarCloud.NET.Models;

public class Comment
{
    [JsonPropertyName("key")]
    public required string Key { get; set; }

    [JsonPropertyName("login")]
    public required string Login { get; set; }
    
    [JsonPropertyName("htmlText")]
    public required string HtmlText { get; set; }
    
    [JsonPropertyName("markdown")]
    public required string Markdown { get; set; }
    
    [JsonPropertyName("createdAt")]
    public required DateTime CreatedAt { get; set; }
}
