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
    public string? Assignee { get; set; }

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
