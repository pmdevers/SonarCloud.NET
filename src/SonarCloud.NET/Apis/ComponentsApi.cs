using SonarCloud.NET.Helpers;
using SonarCloud.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SonarCloud.NET.Apis;

public interface IComponentsApi
{
    Task<ComponentsSearchResponse> Search(ComponentsSearchRequest request, CancellationToken cancellationToken = default);
    Task<ComponentsShowResponse> Show(ComponentsShowRequest request, CancellationToken cancellationToken = default);
    Task<ComponentsTreeResponse> Tree(ComponentsTreeRequest request, CancellationToken cancellationToken = default);
}

public class ComponentsTreeRequest
{
    [QueryString("asc")]
    public bool? SortAcending { get; set; }

    [QueryString("branch")]
    public string? Branch { get; set; }

    [QueryString("component")]
    public required string Component { get; set; }

    [QueryString("pullRequest")]
    public string? PullRequest { get; set; }
}

public class ComponentsShowRequest
{
    [QueryString("branch")]
    public string? Branch { get; set; }

    [QueryString("component")]
    public required string Component { get; set; }

    [QueryString("p")]
    public int? PageNumber { get; set; }

    [QueryString("ps")]
    public int? PageSize { get; set; }

    [QueryString("pullRequest")]
    public string? PullRequest { get; set; }

    [QueryString("q")]
    public string? Query { get; set; }

    [QueryString("qualifiers")]
    public string? Qualifiers { get; set; }

    [QueryString("s")]
    public string? SortFields { get; set; }

    [QueryString("strategy")]
    public string? Strategy { get; set; }
}

public class ComponentsSearchRequest
{
    [QueryString("organization")]
    public required string Organization { get; set; }

    [QueryString("p")]
    public int? PageNumber { get; set; }

    [QueryString("ps")]
    public int? PageSize { get; set; }

    [QueryString("q")]
    public string? Query { get; set; }
}

public class ComponentsTreeResponse : Paged<Component>
{
    [JsonPropertyName("baseComponent")]
    public Component BaseComponent { get; set; } = new();
}

public class ComponentsShowResponse
{
    [JsonPropertyName("component")]
    public Component? Component { get; set; }

    [JsonPropertyName("ancestors")]
    public Component[] Ancestors { get; set; } = [];
}

public class ComponentsSearchResponse : Paged<Component>
{
}

internal sealed class ComponentsApi(SonarCloudApiClient client) : IComponentsApi
{
    private const string endpoint = "api/components";

    public Task<ComponentsSearchResponse> Search(ComponentsSearchRequest request, CancellationToken cancellationToken = default)
        => client.Get<ComponentsSearchRequest, ComponentsSearchResponse>(endpoint + "/search", request, cancellationToken);

    public Task<ComponentsShowResponse> Show(ComponentsShowRequest request, CancellationToken cancellationToken = default)
        => client.Get<ComponentsShowRequest, ComponentsShowResponse>(endpoint + "/show", request, cancellationToken);

    public Task<ComponentsTreeResponse> Tree(ComponentsTreeRequest request, CancellationToken cancellationToken = default)
        => client.Get<ComponentsTreeRequest, ComponentsTreeResponse>(endpoint + "/tree", request, cancellationToken);
}
