using System.Text.Json.Serialization;

namespace SonarCloud.NET.Models;

public class Issue
{
    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;
    [JsonPropertyName("rule")]
    public string Rule { get; set; } = string.Empty;
    [JsonPropertyName("severity")]
    public string Severity { get; set; } = string.Empty;
    [JsonPropertyName("component")]
    public string Component { get; set; } = string.Empty;
    [JsonPropertyName("project")]
    public string Project { get; set; } = string.Empty;
    [JsonPropertyName("line")]
    public int Line { get; set; }
    [JsonPropertyName("textRange")]
    public Textrange TextRange { get; set; } = new Textrange();
    [JsonPropertyName("flows")]
    public object[] Flows { get; set; } = [];
    [JsonPropertyName("issueStatus")]
    public string IssueStatus { get; set; } = string.Empty;
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
    [JsonPropertyName("effort")]
    public string Effort { get; set; } = string.Empty;
    [JsonPropertyName("debt")]
    public string Debt { get; set; } = string.Empty;
    [JsonPropertyName("assignee")]
    public string Assignee { get; set; } = string.Empty;
    [JsonPropertyName("author")]
    public string Author { get; set; } = string.Empty;
    [JsonPropertyName("tags")]
    public string[] Tags { get; set; } = [];
    [JsonPropertyName("transitions")]
    public string[] Transitions { get; set; } = [];
    [JsonPropertyName("actions")]
    public string[] Actions { get; set; } = [];
    [JsonPropertyName("comments")]
    public Comment[] Comments { get; set; } = [];
    [JsonPropertyName("creationDate")]
    public DateTime CreationDate { get; set; }
    [JsonPropertyName("updateDate")]
    public DateTime UpdateDate { get; set; }
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;
    [JsonPropertyName("ruleDescriptionContextKey")]
    public string RuleDescriptionContextKey { get; set; } = string.Empty;
    [JsonPropertyName("cleanCodeAttributeCategory")]
    public string CleanCodeAttributeCategory { get; set; } = string.Empty;
    [JsonPropertyName("cleanCodeAttribute")]
    public string CleanCodeAttribute { get; set; } = string.Empty;
    [JsonPropertyName("impacts")]
    public Impact[] Impacts { get; set; } = [];
}
