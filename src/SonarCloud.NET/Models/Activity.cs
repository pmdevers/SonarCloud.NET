namespace SonarCloud.NET.Models;
public class Activity
{
    public required string Organization { get; set; }
    public required string Id { get; set; }
    public required string Type { get; set; }
    public required string ComponentId { get; set; }
    public required string ComponentKey { get; set; }
    public required string ComponentName { get; set; }
    public required string ComponentQualifier { get; set; }
    public required string AnalysisId { get; set; }
    public required string Status { get; set; }
    public DateTime SubmittedAt { get; set; }
    public required string SubmitterLogin { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime ExecutedAt { get; set; }
    public int ExecutionTimeMs { get; set; }
    public bool Logs { get; set; }
    public bool HasErrorStacktrace { get; set; }
    public bool HasScannerContext { get; set; }
}
