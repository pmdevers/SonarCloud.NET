using System.Net;

namespace SonarCloud.NET.Models;
public class Delivery
{
    public required string Id { get; set; }
    public required string ComponentKey { get; set; }
    public required string CeTaskId { get; set; }
    public required string Name { get; set; }
    public required Uri Url { get; set; }
    public DateTime At { get; set; }
    public bool Success { get; set; }
    public HttpStatusCode HttpStatus { get; set; }
    public int DurationMs { get; set; }
    public string? Payload { get; set; }
}
