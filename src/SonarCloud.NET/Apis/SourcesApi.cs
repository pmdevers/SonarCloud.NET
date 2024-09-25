using SonarCloud.NET.Helpers;
using System;
using System.Text.Json.Serialization;

namespace SonarCloud.NET.Apis;

public interface ISourcesApi
{
    /// <summary>
    /// Get source code as raw text. <br/>
    /// Require 'See Source Code' permission on file
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<string> Raw(SourcesRawRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get SCM information of source files. <br/>
    /// Require See Source Code permission on file's project <br/>
    /// Each element of the result array is composed of:
    /// <list type="number">
    ///   <item>Line number</item>
    ///   <item>Author of the commit</item>
    ///   <item>Datetime of the commit(before 5.2 it was only the Date)</item>
    ///   <item>Revision of the commit(added in 5.2)</item>
    /// </list>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<SourcesSCMResponse> Scm(SourcesSCMRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get source code. <br />
    /// Requires See Source Code permission on file's project, or organization membership on public projects. <br />
    /// Each element of the result array is composed of:
    /// <list type="number">
    ///    <item>Line Number</item>
    ///    <item>Content of the line</item>
    /// </list>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<SourcesShowResponse> Show(SourcesShowRequest request, CancellationToken cancellationToken = default);
}

public class SourcesShowRequest
{
    /// <summary>
    /// First line to return. Starts at 1
    /// </summary>
    [QueryString("from")]
    public int? From { get; init; }

    /// <summary>
    /// File key
    /// </summary>
    [QueryString("key")]
    public required string Key { get; init; }

    /// <summary>
    /// Last line to return (inclusive)
    /// </summary>
    [QueryString("to")]
    public string? To { get; init; }
}

public class SourcesShowResponse
{
    [JsonPropertyName("sources")]
    public object[][] Sources { get; init; } = [];
}

public class SourcesSCMRequest
{
    /// <summary>
    /// Group lines by SCM commit if value is false, else display commits for each line, even if two consecutive lines relate to the same commit.
    /// </summary>
    [QueryString("commits_by_line")]
    public bool? CommitsByLine { get; init; }

    /// <summary>
    /// First line to return. Starts at 1
    /// </summary>
    [QueryString("from")]
    public int? From { get; init; }

    /// <summary>
    /// File key
    /// </summary>
    [QueryString("key")]
    public required string Key { get; init; }

    /// <summary>
    /// Last line to return (inclusive)
    /// </summary>
    [QueryString("to")]
    public string? To { get; init; }
}

public class SourcesSCMResponse
{
    [JsonPropertyName("scm")]
    public object[][] Lines { get; set; } = [];
}

public class SourcesRawRequest
{
    /// <summary>
    /// Branch Key
    /// </summary>
    [QueryString("branch")]
    public string? Branch { get; init; }
    
    /// <summary>
    /// File Key
    /// </summary>
    [QueryString("key")]
    public required string Key{ get; init; }

    /// <summary>
    /// Pull Request Id
    /// </summary>
    [QueryString("pullRequest")]
    public string? PullRequest { get; init; }
}

internal sealed class SourcesApi(SonarCloudApiClient client) : ISourcesApi
{
    public const string Endpoint = "api/sources";

    public Task<string> Raw(SourcesRawRequest request, CancellationToken cancellationToken = default)
        => client.HttpClient.GetStringAsync($"{Endpoint}/raw" + QueryString.AsQueryString(request), cancellationToken);

    public Task<SourcesSCMResponse> Scm(SourcesSCMRequest request, CancellationToken cancellationToken = default)
        => client.Get<SourcesSCMRequest, SourcesSCMResponse>($"{Endpoint}/scm", request, cancellationToken);

    public Task<SourcesShowResponse> Show(SourcesShowRequest request, CancellationToken cancellationToken = default)
        => client.Get<SourcesShowRequest, SourcesShowResponse>($"{Endpoint}/show", request, cancellationToken);
}
