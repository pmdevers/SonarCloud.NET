using System.Text.Json.Serialization;

namespace SonarCloud.NET.Models;

public class Changelog
{
    [JsonPropertyName("user")]
    public required string User { get; set; }
    
    [JsonPropertyName("userName")]
    public required string UserName { get; set; }
    
    [JsonPropertyName("creationDate")]
    public DateTime CreationDate { get; set; }
    
    [JsonPropertyName("diffs")]
    public Diff[] Diffs { get; set; } = [];
    
    [JsonPropertyName("avatar")]
    public required string Avatar { get; set; }

    [JsonPropertyName("isUserActive")]
    public bool IsUserActive { get; set; }

    public class Diff
    {
        [JsonPropertyName("key")]
        public required string Key { get; set; }

        [JsonPropertyName("newValue")]
        public required string NewValue { get; set; }

        [JsonPropertyName("oldValue")]
        public required string OldValue { get; set; }
    }
}
