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
public enum NewCodeDefinitionTypes
{
    previous_version,
    days,
    date,
    version
}

public enum ProjectVisibility
{
    @private,
    @public
}
public class CreateProjectsRequest
{

    [QueryString("name")]
    public required string Name { get; set; }

    [QueryString("newCodeDefinitionType")]
    public NewCodeDefinitionTypes? NewCodeDefinitionType { get; }

    [QueryString("newCodeDefinitionValue")]
    public string? NewCodeDefinitionValue { get; }

    [QueryString("organization")]
    public required string Organization { get; set; }

    [QueryString("projects")]
    public string? Projects { get; set; }

    [QueryString("visibility")]
    public ProjectVisibility? Visibility { get; set; }
}
public class UpdateVisibilityRequest
{
    [QueryString("projects")]
    public required string Projects { get; set; }
    
    [QueryString("visibility")]
    public required ProjectVisibility Visibility { get; set; }
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
    [QueryString("ps")]
    public int PageSize { get; set; } = 100;

    [QueryString("q")]
    public string? Query { get; set; }
}

internal sealed class ProjectApi(SonarCloudApiClient client) : IProjectsApi
{
    private const string endpoint = "api/projects";

    public async Task BulkDelete(BulkDeleteRequest request, CancellationToken token = default)
    {
        var response = await client.HttpClient.PostAsJsonAsync($"{endpoint}/bulk_delete" + QueryString.ToQueryString(request), token)!;
        SonarCloudApiClient.HandleErrors(response);
    }

    public async Task<CreateProjectsResponse> Create(CreateProjectsRequest request, CancellationToken token = default)
    {
        var response = await client.HttpClient.PostAsJsonAsync($"{endpoint}/create" + QueryString.ToQueryString(request), token);
        var result = await client.HandleResponseAsync<CreateProjectsResponse>(response, token);
        return result;
    }

    public async Task Delete(DeleteProjectsRequest request, CancellationToken token = default)
    {
        var response = await client.HttpClient.PostAsJsonAsync($"{endpoint}/bulk_delete" + QueryString.ToQueryString(request), token)!;
        SonarCloudApiClient.HandleErrors(response);
    }

    public async Task<SearchProjectsResponse> Search(SearchProjectsRequest request, CancellationToken token = default)
    {
        var response = await client.HttpClient.PostAsJsonAsync($"{endpoint}/search" + QueryString.ToQueryString(request), token);
        var result = await client.HandleResponseAsync<SearchProjectsResponse>(response, token);
        return result;
    }

    public async Task UpdateKey(UpdateKeyRequest request, CancellationToken token = default)
    {
        var response = await client.HttpClient.PostAsJsonAsync($"{endpoint}/update_key" + QueryString.ToQueryString(request), token)!;
        SonarCloudApiClient.HandleErrors(response);
    }

    public async Task UpdateVisibility(UpdateVisibilityRequest request, CancellationToken token = default)
    {
        var response = await client.HttpClient.PostAsJsonAsync($"{endpoint}/update_visibility" + QueryString.ToQueryString(request), token)!;
        SonarCloudApiClient.HandleErrors(response);
    }
}
