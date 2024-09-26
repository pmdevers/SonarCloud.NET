using SonarCloud.NET.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarCloud.NET.Apis;

/// <summary>
/// Get and update some details of automatic rules, and manage custom rules.
/// </summary>
public interface IRulesApi
{
    /// <summary>
    /// List available rule repositories
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<RulesRepositoriesResponse> Repositories(RulesRepositoriesRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Search for a collection of relevant rules matching a specified query.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<RulesSearchResponse> Search(RulesSearchRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get detailed information about a rule.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<RulesShowResponse> Show(RulesShowRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// List rule tags
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<RulesTagsResponse> Tags(RulesTagsRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update an existing rule.
    /// Requires the 'Administer Quality Profiles' permission
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<RulesUpdateReponse> Update(RulesUpdateRequest request, CancellationToken cancellationToken = default);
}

public class RulesUpdateRequest
{
}

public class RulesUpdateReponse
{
}

public class RulesTagsRequest
{
}

public class RulesTagsResponse
{
}

public class RulesShowRequest
{
}

public class RulesShowResponse
{
}

public class RulesSearchRequest
{

}

public class RulesSearchResponse
{
    [QueryString("activation")]
    public bool? Activation { get; init; }

    [QueryString("active_severities")]
    public string? ActiveSeverities { get; init; }

    [QueryString("asc")]
    public bool? Ascending { get; set; }

    [QueryString("available_since")]
    public DateOnly? AvailableSince { get; init; }

    [QueryString("cleanCodeAttributeCategories")]
    public string? CleanCodeAttributeCategories { get; init; }

    [QueryString("cwe")]
    public string? CWEIdentifiers { get; init; }

    [QueryString("f")]
    public string? Fields { get; init; }

    [QueryString("facets")]
    public string? Facets { get; init; }

    [QueryString("impactSeverities")]
    public string? ImpactSeverities { get; init; }

    [QueryString("impactSoftwareQualities")]
    public string? ImpactSoftwareQualities { get; init; }

    [QueryString("include_external")]
    public bool? IncludeExternal { get; init; }

    [QueryString("inheritance")]
    public string? Inheritence { get; init; }

    [QueryString("is_template")]
    public bool? IsTemplate { get; init; }

    [QueryString("languages")]
    public string? Languages { get; init; }

    [QueryString("organization")]
    public string? Organization { get; init; }

    [QueryString("owaspTop10")]
    public string? OwaspTop10 { get; init; }

    [QueryString("p")]
    public int? PageNumber { get; init; }

    [QueryString("ps")]
    public int? PageSize { get; init; }

    [QueryString("q")]
    public string? Query { get; init; }

    [QueryString("qprofile")]
    public string? QualityProfile { get; init; }

    [QueryString("repositories")]
    public string? Repositories { get; init; }

    [QueryString("rule_key")]
    public string? RuleKey { get; init; }

    [QueryString("rule_keys")]
    public string? RuleKeys { get; init; }


}

public class RulesRepositoriesRequest
{
    /// <summary>
    /// A language key; if provided, only repositories for the given language will be returned
    /// </summary>
    [QueryString("language")]
    public string? Language { get; init; }

    /// <summary>
    /// A pattern to match repository keys/names against
    /// </summary>
    [QueryString("q")]
    public string? Query { get; init; }
}

public class RulesRepositoriesResponse
{
    public required RepositoryInfo[] Repositories { get; init; }
}

public record RepositoryInfo(string Key, string Name, string Language);

internal sealed class RulesApi(SonarCloudApiClient client) : IRulesApi
{
    private const string Endpoint = "api/rules";

    public Task<RulesRepositoriesResponse> Repositories(RulesRepositoriesRequest request, CancellationToken cancellationToken = default)
        => client.Get<RulesRepositoriesRequest, RulesRepositoriesResponse>($"{Endpoint}/repositories", request, cancellationToken);

    public Task<RulesSearchResponse> Search(RulesSearchRequest request, CancellationToken cancellationToken = default) 
        => client.Get<RulesSearchRequest, RulesSearchResponse>($"{Endpoint}/search", request, cancellationToken);

    public Task<RulesShowResponse> Show(RulesShowRequest request, CancellationToken cancellationToken = default)
        => client.Get<RulesShowRequest, RulesShowResponse>($"{Endpoint}/show", request, cancellationToken);

    public Task<RulesTagsResponse> Tags(RulesTagsRequest request, CancellationToken cancellationToken = default)
        => client.Get<RulesTagsRequest, RulesTagsResponse>($"{Endpoint}/tags", request, cancellationToken);

    public Task<RulesUpdateReponse> Update(RulesUpdateRequest request, CancellationToken cancellationToken = default)
        => client.Post<RulesUpdateRequest, RulesUpdateReponse>($"{Endpoint}/update", request, cancellationToken);
}
