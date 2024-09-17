using System.Text.Json.Serialization;

namespace SonarCloud.NET.Models;
public class UserToken
{
    [JsonPropertyName("name")]
    public required string Name { get;set; }
    
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }
}
