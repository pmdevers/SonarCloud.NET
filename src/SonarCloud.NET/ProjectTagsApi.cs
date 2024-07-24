using SonarCloud.NET.Helpers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace SonarCloud.NET;

public interface IProjectTagsApi
{
    Task<SearchProjectTagsResponse> Search(SearchProjectTagsRequest request, CancellationToken token = default);
    Task SetTags(SetProjectTagsRequest request, CancellationToken token = default);
}

public class SearchProjectTagsResponse
{
    [JsonPropertyName("tags")]
    public string[] Tags { get; set; } = [];
}

public class SetProjectTagsRequest
{
    [QueryString("project")]
    public required string ProjectKey { get; set; }

    [QueryString("tags")]
    public required string[] Tags { get; set; }
}

public class SearchProjectTagsRequest
{
    [QueryString("ps")]
    public int? PageSize { get; set; }

    [QueryString("q")]
    public string? Query { get; set; }
}

internal sealed class ProjectTagsApi(SonarCloudApiClient client) : IProjectTagsApi
{
    private const string endpoint = "api/project_tags";
    public async Task<SearchProjectTagsResponse> Search(SearchProjectTagsRequest request, CancellationToken token = default)
    {
        var response = await client.HttpClient.PostAsJsonAsync($"{endpoint}/search" + QueryString.ToQueryString(request), token);
        var result = await client.HandleResponseAsync<SearchProjectTagsResponse>(response, token);
        return result;
    }

    public async Task SetTags(SetProjectTagsRequest request, CancellationToken token = default)
    {
        var response = await client.HttpClient.PostAsJsonAsync($"{endpoint}/set" + QueryString.ToQueryString(request), token)!;
        SonarCloudApiClient.HandleErrors(response);
    }
}
