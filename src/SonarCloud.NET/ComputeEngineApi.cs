using SonarCloud.NET.Helpers;
using SonarCloud.NET.Models;
using System.Text.Json.Serialization;

namespace SonarCloud.NET;

public interface IComputeEngineApi
{
    Task<SearchForTasksResponse> Search(SearchForTasksRequest request, CancellationToken cancellationToken = default);
    Task<GetActivityStatusResponse> GetActivityStatus(GetActivityStatusRequest request, CancellationToken cancellationToken = default);
    Task<PendingTasksResponse> GetPendingTasks(GetPendingTasksRequest request, CancellationToken cancellationToken = default);
    Task<TaskDetailsResponse> GetTaskDetails(GetTaskDetailsRequest request, CancellationToken cancellationToken = default);
}

public class GetPendingTasksRequest
{
    [QueryString("component")]
    public string? Component { get; set; }
}

public class PendingTasksResponse
{
    [JsonPropertyName("queue")]
    public ComputeTask[] Queue { get; set; } = [];
    [JsonPropertyName("current")]
    public ComputeTask? Current { get; set;}

    
}

public class GetTaskDetailsRequest
{
    [QueryString("id")]
    public required string Id { get; set; }

    [QueryString("additionalFields")]
    public string? AdditionalFields { get; set; }
}

public class TaskDetailsResponse
{
    [JsonPropertyName("task")]
    public ComputeTask? Task { get; set; }
}

public class GetActivityStatusRequest
{
    [QueryString("componentId")]
    public string? ComponentId { get; set; }
}

public class GetActivityStatusResponse
{
    [JsonPropertyName("pending")]
    public int Pending { get; set; }
    [JsonPropertyName("inProgress")]
    public int InProgress { get; set; }
    [JsonPropertyName("failing")]
    public int Failing { get; set; }
    [JsonPropertyName("pendingTime")]
    public int PendingTime { get; set; }
}

public class SearchForTasksRequest
{
    [QueryString("component")]
    public string? Component { get; set; }

    [QueryString("maxExecutedAt")]
    public DateTime? MaxExecutedAt { get; set; }

    [QueryString("minSubmittedAt")]
    public DateTime? MinSubmittedAt { get; set; }

    [QueryString("onlyCurrents")]
    public bool? OnlyCurrents { get; set; }

    [QueryString("ps")]
    public int? PageSize { get; set; }

    [QueryString("q")]
    public string? Query { get; set; }

    [QueryString("status")]
    public string? Status { get; set; }

    [QueryString("type")]
    public string? Type { get; set; }
}
public class SearchForTasksResponse
{
    [JsonPropertyName("tasks")]
    public ComputeTask[] Tasks { get; set; } = [];
}

internal sealed class ComputeEngineApi(SonarCloudApiClient client) : IComputeEngineApi
{
    public const string Endpoint = "/api/ce";

    public Task<GetActivityStatusResponse> GetActivityStatus(GetActivityStatusRequest request, CancellationToken cancellationToken = default)
        => client.Get<GetActivityStatusRequest, GetActivityStatusResponse>($"{Endpoint}/activity_status", request, cancellationToken);

    public Task<PendingTasksResponse> GetPendingTasks(GetPendingTasksRequest request, CancellationToken cancellationToken = default)
        => client.Get<GetPendingTasksRequest, PendingTasksResponse>($"{Endpoint}/component", request, cancellationToken);

    public Task<TaskDetailsResponse> GetTaskDetails(GetTaskDetailsRequest request, CancellationToken cancellationToken = default)
        => client.Get<GetTaskDetailsRequest, TaskDetailsResponse>($"{Endpoint}/task", request, cancellationToken);

    public Task<SearchForTasksResponse> Search(SearchForTasksRequest request, CancellationToken cancellationToken = default)
        => client.Get<SearchForTasksRequest, SearchForTasksResponse>($"{Endpoint}/activity", request, cancellationToken);
}
