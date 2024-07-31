using System.Text.Json.Serialization;

namespace SonarCloud.NET.Models;

/// <summary>
/// Security Hotspot
/// </summary>
public class Hotspot
{
    /// <summary>
    /// Key of the Security Hotspot
    /// </summary>
    [JsonPropertyName("key")]
    public required string Key { get; set; }

    /// <summary>
    /// Component key
    /// </summary>
    [JsonPropertyName("component")]
    public required string Component { get; set; }

    /// <summary>
    /// Project Key
    /// </summary>
    [JsonPropertyName("project")]
    public required string Project { get; set; }

    /// <summary>
    /// Security Category
    /// </summary>
    [JsonPropertyName("securityCategory")]
    public required string SecurityCategory { get; set; }

    /// <summary>
    /// vulnerabilityProbability
    /// </summary>
    [JsonPropertyName("vulnerabilityProbability")]
    public required string VulnerabilityProbability { get; set; }

    /// <summary>
    /// review status.
    /// </summary>
    [JsonPropertyName("status")]
    public required string Status { get; set; }

    /// <summary>
    /// Line of code
    /// </summary>
    [JsonPropertyName("line")]
    public int Line { get; set; }

    /// <summary>
    /// The message.
    /// </summary>
    [JsonPropertyName("message")]
    public required string Message { get; set; }

    /// <summary>
    /// assignee identifier
    /// </summary>
    [JsonPropertyName("assignee")]
    public required string Assignee { get; set; }

    /// <summary>
    /// Name of the Author
    /// </summary>
    [JsonPropertyName("author")]
    public required string Author { get; set; }

    /// <summary>
    /// Creation Date.
    /// </summary>
    [JsonPropertyName("creationDate")]
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// Update date.
    /// </summary>
    [JsonPropertyName("updateDate")]
    public DateTime UpdateDate { get; set; }

    /// <summary>
    /// Text range
    /// </summary>
    [JsonPropertyName("textRange")]
    public required Textrange TextRange { get; set; }

    /// <summary>
    /// The rule key.
    /// </summary>
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
