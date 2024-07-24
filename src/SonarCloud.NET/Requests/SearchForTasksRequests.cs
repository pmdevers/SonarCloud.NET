using SonarCloud.NET.Helpers;
using System.Text.Json.Serialization;

namespace SonarCloud.NET.Requests;
public class SearchForTasksRequest
{
    [QueryString("component")]
    public string? Component { get; set; }

    [QueryString("maxExecutedAt")]
    public DateTime? MaxExecutedAt { get; set; }

    [QueryString("minSubmittedAt")]
    public DateTime? MinSubmittedAt { get; set; }

    [QueryString("onlyCurrents")]
    public bool OnlyCurrents { get; set; }

    [QueryString("ps")]
    public int PageSize { get; set; } = 100;

    [QueryString("q")]
    public string? Query { get; set; }

    [QueryString("status")]
    public string[] Status { get; set; } = ["SUCCESS", "FAILED", "CANCELED"];

    [QueryString("type")]
    public string Type { get; set; } = "REPORT";
}
