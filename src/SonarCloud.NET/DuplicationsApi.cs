using SonarCloud.NET.Helpers;
using SonarCloud.NET.Models;
using System.Text.Json.Serialization;

namespace SonarCloud.NET;

public interface IDuplicationsApi
{
    Task<DuplicationsShowResponse> Show(DuplicationsShowRequest request, CancellationToken cancellationToken = default);
}

public class DuplicationsShowResponse
{
    [JsonPropertyName("duplications")]
    public Duplication[] Duplications { get;set;} = [];

    [JsonPropertyName("files")]
    public Dictionary<string, SourceFile> Files { get; set; } = [];
}

public class DuplicationsShowRequest
{
    [QueryString("branch")]
    public string? Branch { get; set; }
    [QueryString("key")]
    public string? Key { get; set; }

    [QueryString("pullRequest")]
    public string? PullRequest { get; set; }
}

internal class DuplicationsApi(SonarCloudApiClient client) : IDuplicationsApi
{
    private const string endpoint = "api/duplications";

    public Task<DuplicationsShowResponse> Show(DuplicationsShowRequest request, CancellationToken cancellationToken = default)
        => client.Get<DuplicationsShowRequest, DuplicationsShowResponse>(endpoint + "/show", request, cancellationToken);
}
