using SonarCloud.NET.Helpers;
using SonarCloud.NET.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json.Serialization;
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
    /// <summary>
    /// Key of the rule to update
    /// </summary>
    [QueryString("key")]
    public required string Key { get; init; }

    /// <summary>
    /// Rule description (mandatory for custom rule and manual rule)
    /// </summary>
    [QueryString("markdown_description")]
    public string? Description { get; init; }

    /// <summary>
    /// Optional note in markdown format. Use empty value to remove current note. Note is not changed if the parameter is not set.
    /// </summary>
    [QueryString("markdown_note")]
    public string? Note { get; init; }

    /// <summary>
    /// Rule name (mandatory for custom rule)
    /// </summary>
    [QueryString("name")]
    public string? Name { get; init; }

    /// <summary>
    /// Organization key
    /// </summary>
    [QueryString("organization")]
    public required string Organization { get; init; }

    /// <summary>
    /// Parameters as semi-colon list of =, for example 'params=key1=v1;key2=v2' (Only when updating a custom rule)
    /// </summary>
    [QueryString("params")]
    public string? Params { get; init; }

    /// <summary>
    /// Base effort of the remediation function of the rule
    /// </summary>
    [QueryString("remediation_fn_base_effort")]
    public string? RemediationBaseEffort { get; init; }

    /// <summary>
    /// Type of the remediation function of the rule
    /// </summary>
    [QueryString("remediation_fn_type")]
    public string? RemediationType { get; init; }

    /// <summary>
    /// Gap multiplier of the remediation function of the rule
    /// </summary>
    [QueryString("remediation_fy_gap_multiplier")]
    public string? RemediationGapMultiplier { get; init; }

    /// <summary>
    /// Rule severity (Only when updating a custom rule)
    /// </summary>
    [QueryString("severity")]
    public string? Severity { get; init; }

    /// <summary>
    /// Rule status (Only when updating a custom rule)
    /// </summary>
    [QueryString("status")]
    public string? Status { get; init; }

    /// <summary>
    /// Optional comma-separated list of tags to set. Use blank value to remove current tags. Tags are not changed if the parameter is not set.
    /// </summary>
    [QueryString("tags")]
    public string? Tags { get; init; }
}

public class RulesUpdateReponse
{
    [JsonPropertyName("rule")]
    public required RuleInfo Rule { get; init; }
}

public class RulesTagsRequest
{
    /// <summary>
    /// Organization key
    /// </summary>
    [QueryString("organization")]
    public required string Organization { get; init; }

    /// <summary>
    /// Page size. Must be greater than 0 and less or equal than 100
    /// </summary>
    [QueryString("ps")]
    public int? PageSize { get; init; }

    /// <summary>
    /// Limit search to tags that contain the supplied string.
    /// </summary>
    [QueryString("q")]
    public string? Query { get; init; }
}

public class RulesTagsResponse
{
    [JsonPropertyName("tags")]
    public string[] Tags { get; init; } = [];
}

public class RulesShowRequest
{
    /// <summary>
    /// Show rule's activations for all profiles ("active rules")
    /// </summary>
    [QueryString("actives")]
    public bool? Actives { get; set; }

    /// <summary>
    /// Rule key
    /// </summary>
    [QueryString("key")]
    public required string Key { get; set; }

    /// <summary>
    /// Organization key
    /// </summary>
    [QueryString("organization")]
    public required string Organization { get; set; }
}

public class RulesShowResponse
{
    [JsonPropertyName("rule")]
    public required RuleInfo Rule { get; init; }

    [JsonPropertyName("actives")]
    public object[] Actives { get; set; } = [];
}

public class RulesSearchResponse
{
    [JsonPropertyName("total")]
    public required int Total { get; init; }

    [JsonPropertyName("p")]
    public required int Page { get; init; }

    [JsonPropertyName("ps")]
    public required int PageSize { get; init; }

    [JsonPropertyName("rules")]
    public required RuleInfo[] Rules { get; init; }
}

public class RuleInfo
{
    [JsonPropertyName("key")]
    public required string Key { get; init; }

    [JsonPropertyName("repo")]
    public required string Repository { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; init; }

    [JsonPropertyName("htmlDesc")]
    public required string HtmlDescription { get; init; }

    [JsonPropertyName("severity")]
    public required string Severity { get; init; }

    [JsonPropertyName("status")]
    public required string Status { get; init; }

    [JsonPropertyName("internalKey")]
    public string? InternalKey { get; init; }

    [JsonPropertyName("isTemplate")]
    public bool IsTemplate { get; init; }

    [JsonPropertyName("tags")]
    public string[] Tags { get; init; } = [];

    [JsonPropertyName("sysTags")]
    public string[] SysTags { get; init; } = [];

    [JsonPropertyName("lang")]
    public required string Language { get; init; }

    [JsonPropertyName("langName")]
    public required string LanguageName { get; init; }

    [JsonPropertyName("scope")]
    public required string Scope { get; init; }

    [JsonPropertyName("isExternal")]
    public bool IsExternal { get; init; }

    [JsonPropertyName("type")]
    public required string Type { get; init; }

    [JsonPropertyName("descriptionSections")]
    public DescriptionSection[] DescriptionSections { get; init; } = [];

    [JsonPropertyName("params")]
    public RuleParameter[] Parameters { get; init; } = [];

    public class DescriptionSection
    {
        [JsonPropertyName("key")]
        public required string Key { get; init; }
        [JsonPropertyName("content")]
        public required string Content { get; init; }

        [JsonPropertyName("context")]
        public ContextInfo? Context { get; init; }

        public class ContextInfo
        {
            [JsonPropertyName("displayName")]
            public required string DisplayName { get; init; }

            [JsonPropertyName("key")]
            public required string Key { get; init; }
        }
    }

    public class RuleParameter
    {
        [JsonPropertyName("key")]
        public required string Key { get; set; }
        
        [JsonPropertyName("desc")]
        public string? Description { get; set; }

        [JsonPropertyName("defaultValue")]
        public string? DefaultValue { get; set; }
    }
}

public class RulesSearchRequest 
{
    /// <summary>
    /// Filter rules that are activated or deactivated on the selected Quality profile. Ignored if the parameter 'qprofile' is not set.
    /// </summary>
    [QueryString("activation")]
    public bool? Activation { get; init; }

    /// <summary>
    /// Comma-separated list of activation severities, i.e the severity of rules in Quality profiles.
    /// </summary>
    [QueryString("active_severities")]
    public string? ActiveSeverities { get; init; }

    /// <summary>
    /// Ascending sort
    /// </summary>
    [QueryString("asc")]
    public bool? Ascending { get; set; }

    /// <summary>
    /// Filters rules added since date. Format is yyyy-MM-dd
    /// </summary>
    [QueryString("available_since")]
    public DateOnly? AvailableSince { get; init; }

    /// <summary>
    /// Comma-separated list of Clean Code Attribute Categories
    /// </summary>
    [QueryString("cleanCodeAttributeCategories")]
    public string? CleanCodeAttributeCategories { get; init; }

    /// <summary>
    /// Comma-separated list of CWE identifiers. Use 'unknown' to select rules not associated to any CWE.
    /// </summary>
    [QueryString("cwe")]
    public string? CWEIdentifiers { get; init; }

    /// <summary>
    /// Comma-separated list of the fields to be returned in response. All the fields are returned by default, except actives.
    /// </summary>
    [QueryString("f")]
    public string? Fields { get; init; }

    /// <summary>
    /// Comma-separated list of the facets to be computed. No facet is computed by default.
    /// </summary>
    [QueryString("facets")]
    public string? Facets { get; init; }

    /// <summary>
    /// Comma-separated list of Software Quality Severities
    /// </summary>
    [QueryString("impactSeverities")]
    public string? ImpactSeverities { get; init; }

    /// <summary>
    /// Comma-separated list of Software Qualities
    /// </summary>
    [QueryString("impactSoftwareQualities")]
    public string? ImpactSoftwareQualities { get; init; }

    /// <summary>
    /// Include external engine rules in the results
    /// </summary>
    [QueryString("include_external")]
    public bool? IncludeExternal { get; init; }

    /// <summary>
    /// Comma-separated list of values of inheritance for a rule within a quality profile. Used only if the parameter 'activation' is set.
    /// </summary>
    [QueryString("inheritance")]
    public string? Inheritence { get; init; }

    /// <summary>
    /// Filter template rules
    /// </summary>
    [QueryString("is_template")]
    public bool? IsTemplate { get; init; }

    /// <summary>
    /// Comma-separated list of languages
    /// </summary>
    [QueryString("languages")]
    public string? Languages { get; init; }

    /// <summary>
    /// Organization key
    /// </summary>
    [QueryString("organization")]
    public string? Organization { get; init; }

    /// <summary>
    /// Comma-separated list of OWASP Top 10 lowercase categories.
    /// </summary>
    [QueryString("owaspTop10")]
    public string? OwaspTop10 { get; init; }

    /// <summary>
    /// 1-based page number
    /// </summary>
    [QueryString("p")]
    public int? PageNumber { get; init; }

    /// <summary>
    /// Page size. Must be greater than 0 and less or equal than 500
    /// </summary>
    [QueryString("ps")]
    public int? PageSize { get; init; }

    /// <summary>
    /// UTF-8 search query
    /// </summary>
    [QueryString("q")]
    public string? Query { get; init; }

    /// <summary>
    /// Quality profile key to filter on. Used only if the parameter 'activation' is set.
    /// </summary>
    [QueryString("qprofile")]
    public string? QualityProfile { get; init; }

    /// <summary>
    /// Comma-separated list of repositories
    /// </summary>
    [QueryString("repositories")]
    public string? Repositories { get; init; }

    /// <summary>
    /// Key of rule to search for
    /// </summary>
    [QueryString("rule_key")]
    public string? RuleKey { get; init; }

    /// <summary>
    /// Rule keys
    /// </summary>
    [QueryString("rule_keys")]
    public string? RuleKeys { get; init; }

    /// <summary>
    /// Sort field
    /// </summary>
    [QueryString("s")]
    public string? SortField { get; init; }

    /// <summary>
    /// Comma-separated list of SANS Top 25 categories.
    /// </summary>
    [QueryString("sansTop25")]
    public string? SansTop25 { get; init; }

    /// <summary>
    /// Comma-separated list of default severities. Not the same than severity of rules in Quality profiles.
    /// </summary>
    [QueryString("severities")]
    public string? Severities { get; init; }

    /// <summary>
    /// Comma-separated list of SonarSource security categories. Use 'others' to select rules not associated with any category
    /// </summary>
    [QueryString("sonarSourceSecurity")]
    public string? SonarSourceSecurity { get; init; }

    /// <summary>
    /// Comma-separated list of status codes
    /// </summary>
    [QueryString("statuses")]
    public string? Statuses { get; init; }

    /// <summary>
    /// Comma-separated list of tags. Returned rules match any of the tags (OR operator)
    /// </summary>
    [QueryString("tags")]
    public string? Tags { get; init; }

    /// <summary>
    /// Key of the template rule to filter on. Used to search for the custom rules based on this template.
    /// </summary>
    [QueryString("template_key")]
    public string? TemplateKey { get; init; }

    /// <summary>
    /// Comma-separated list of types. Returned rules match any of the tags (OR operator)
    /// </summary>
    [QueryString("types")]
    public string? Types { get; init; }
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
