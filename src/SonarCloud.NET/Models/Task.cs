using System.Text.Json.Serialization;

namespace SonarCloud.NET.Models;
public class ComputeTask
{
    [JsonPropertyName("organization")]
    public string Organization { get; set; } = string.Empty;

    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("componentId")]
    public string ComponentId { get; set; } = string.Empty;

    [JsonPropertyName("componentKey")]
    public string ComponentKey { get; set; } = string.Empty;

    [JsonPropertyName("componentName")]
    public string ComponentName { get; set; } = string.Empty;

    [JsonPropertyName("componentQualifier")]
    public string ComponentQualifier { get; set; } = string.Empty;

    [JsonPropertyName("analysisId")]
    public string AnalysisId { get; set; } = string.Empty;

    [JsonPropertyName("branch")]
    public string Branch { get; set; } = string.Empty;

    [JsonPropertyName("branchType")]
    public string BranchType { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("submittedAt")]
    public DateTime SubmittedAt { get; set; }

    [JsonPropertyName("startedAt")]
    public DateTime StartedAt { get; set; }

    [JsonPropertyName("finishedAt")]
    public DateTime FinishedAt { get; set; }

    [JsonPropertyName("executionTimeMs")]
    public int ExecutionTimeMs { get; set; }

    [JsonPropertyName("errorMessage")]
    public string? ErrorMessage { get; set; } = string.Empty;

    [JsonPropertyName("errorType")]
    public string? ErrorType { get; set; } = string.Empty;

    [JsonPropertyName("hasErrorStacktrace")]
    public bool HasErrorStacktrace { get; set; }

    [JsonPropertyName("hasScannerContext")]
    public bool HasScannerContext { get; set; }
}
