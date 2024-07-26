using SonarCloud.NET.Helpers;
using SonarCloud.NET.Models;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace SonarCloud.NET;

public interface IProjectsApi
{
    public Task BulkDelete(BulkDeleteRequest request, CancellationToken token = default);
    public Task<CreateProjectsResponse> Create(CreateProjectsRequest request, CancellationToken token = default);
    public Task Delete(DeleteProjectsRequest request, CancellationToken token = default);
    public Task<SearchProjectsResponse> Search(SearchProjectsRequest request, CancellationToken token = default);
    public Task UpdateKey(UpdateKeyRequest request, CancellationToken token = default);
    public Task UpdateVisibility(UpdateVisibilityRequest request, CancellationToken token = default);
}

public class BulkDeleteRequest
{
    [QueryString("analyzedBefore")]
    public DateTime? AnalyzedBefore { get; set; }

    [QueryString("onProvisionedOnly")]
    public bool? OnProvisinedOnly { get; set; }

    [QueryString("organization")]
    public required string Organization { get; set; }

    [QueryString("projects")]
    public string? Projects { get; set; }

    [QueryString("q")]
    public string? Query { get; set; }
}

public class CreateProjectsRequest
{
    [QueryString("organization")]
    public required string Organization { get; set; }

    [QueryString("project")]
    public required string Project { get; set; }

    [QueryString("name")]
    public required string Name { get; set; }

    [QueryString("newCodeDefinitionType")]
    public string? NewCodeDefinitionType { get; }

    [QueryString("newCodeDefinitionValue")]
    public string? NewCodeDefinitionValue { get; }

    [QueryString("visibility")]
    public string? Visibility { get; set; }
}
public class UpdateVisibilityRequest
{
    [QueryString("projects")]
    public required string Projects { get; set; }
    
    [QueryString("visibility")]
    public required string Visibility { get; set; }
}

public class UpdateKeyRequest
{
    [QueryString("from")]
    public required string From { get; set; }

    [QueryString("to")]
    public required string To { get; set; }
}

public class SearchProjectsResponse : Paged<Project>
{
}

public class DeleteProjectsRequest
{
    [QueryString("projects")]
    public string? Projects { get; set; }
}

public class CreateProjectsResponse
{
    [JsonPropertyName("project")]
    public Project Project { get; set; } = Project.Empty;
}

public class SearchProjectsRequest
{
    [QueryString("analyzedBefore")]
    public DateTime? AnalyzedBefore { get;set; }

    [QueryString("onProvisionedOnly")]
    public bool? OnProvisionedOnly { get; set; }

    [QueryString("organization")]
    public required string Organization { get; set; }

    [QueryString("p")]
    public int? Page { get; set; }

    [QueryString("projects")]
    public string? Projects { get; set; }

    [QueryString("ps")]
    public int? PageSize { get; set; }

    [QueryString("q")]
    public string? Query { get; set; }
}

internal sealed class ProjectsApi(SonarCloudApiClient client) : IProjectsApi
{
    private const string endpoint = "api/projects";

    public Task BulkDelete(BulkDeleteRequest request, CancellationToken token = default)
        => client.Post($"{endpoint}/bulk_delete", request, token);

    public Task<CreateProjectsResponse> Create(CreateProjectsRequest request, CancellationToken token = default)
        => client.Post<CreateProjectsRequest, CreateProjectsResponse>($"{endpoint}/create", request, token);

    public Task Delete(DeleteProjectsRequest request, CancellationToken token = default)
        => client.Post($"{endpoint}/bulk_delete", request, token);

    public Task<SearchProjectsResponse> Search(SearchProjectsRequest request, CancellationToken token = default)
        => client.Get<SearchProjectsRequest, SearchProjectsResponse>($"{endpoint}/search", request, token);
    
    public Task UpdateKey(UpdateKeyRequest request, CancellationToken token = default)
        => client.Post($"{endpoint}/update_key", request, token);

    public Task UpdateVisibility(UpdateVisibilityRequest request, CancellationToken token = default)
        => client.Post($"{endpoint}/update_visibility", request, token);
}
