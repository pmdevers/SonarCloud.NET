using System.Text.Json.Serialization;

namespace SonarCloud.NET.Models;

public class Hotspot
{
    [JsonPropertyName("key")]
    public required string Key { get; set; }

    [JsonPropertyName("component")]
    public required string Component { get; set; }

    [JsonPropertyName("project")]
    public required string Project { get; set; }

    [JsonPropertyName("securityCategory")]
    public required string SecurityCategory { get; set; }

    [JsonPropertyName("vulnerabilityProbability")]
    public required string VulnerabilityProbability { get; set; }

    [JsonPropertyName("status")]
    public required string Status { get; set; }

    [JsonPropertyName("line")]
    public int Line { get; set; }

    [JsonPropertyName("message")]
    public required string Message { get; set; }

    [JsonPropertyName("assignee")]
    public required string Assignee { get; set; }

    [JsonPropertyName("author")]
    public required string Author { get; set; }

    [JsonPropertyName("creationDate")]
    public DateTime CreationDate { get; set; }

    [JsonPropertyName("updateDate")]
    public DateTime UpdateDate { get; set; }

    [JsonPropertyName("textRange")]
    public required Textrange TextRange { get; set; }

    [JsonPropertyName("ruleKey")]
    public required string RuleKey { get; set; }
}

public class Textrange
{
    [JsonPropertyName("startLine")]
    public int StartLine { get; set; }
    [JsonPropertyName("endLine")]
    public int EndLine { get; set; }
    [JsonPropertyName("startOffset")]
    public int StartOffset { get; set; }
    [JsonPropertyName("endOffset")]
    public int EndOffset { get; set; }
}

public class Rule
{
    [JsonPropertyName("key")]
    public required string Key { get; set; }
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("securityCategory")]
    public required string SecurityCategory { get; set; }
    [JsonPropertyName("vulnerabilityProbability")]
    public required string VulnerabilityProbability { get; set; }
}

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

public class User
{
    [JsonPropertyName("login")]
    public required string Login { get; set; }
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("active")]
    public required bool Active { get; set; }
}
