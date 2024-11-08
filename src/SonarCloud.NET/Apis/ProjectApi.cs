﻿using SonarCloud.NET.Helpers;
using SonarCloud.NET.Models;
using System.Text.Json.Serialization;

namespace SonarCloud.NET.Apis;

public interface IProjectsApi
{
    /// <summary>
    /// Delete one or several projects.
    /// Only the 1000 first items in project filters are taken into account.
    /// Requires 'Administer System' permission.
    /// At least one parameter is required among AnalyzedBefore, ProjectKeys and Query
    /// </summary>
    public Task BulkDelete(BulkDeleteRequest request, CancellationToken token = default);
    /// <summary>
    /// Create a project.
    /// Requires 'Create Projects' permission
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<CreateProjectsResponse> Create(CreateProjectsRequest request, CancellationToken token = default);

    /// <summary>
    /// Delete a project.
    /// Requires 'Administer System' permission or 'Administer' permission on the project.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task Delete(DeleteProjectsRequest request, CancellationToken token = default);
    /// <summary>
    /// Search for projects to administrate them.
    /// Requires 'System Administrator' permission
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<SearchProjectsResponse> Search(SearchProjectsRequest request, CancellationToken token = default);
    /// <summary>
    /// Update a project or module key and all its sub-components keys.
    /// Requires the permission 'Administer' on the specified project.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task UpdateKey(UpdateKeyRequest request, CancellationToken token = default);
    /// <summary>
    /// Updates visibility of a project.
    /// Requires 'Project administer' permission on the specified project
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task UpdateVisibility(UpdateVisibilityRequest request, CancellationToken token = default);
}

public class BulkDeleteRequest
{
    /// <summary>
    /// Filter the projects for which last analysis is older than the given date (exclusive).
    /// </summary>
    [QueryString("analyzedBefore")]
    public DateTime? AnalyzedBefore { get; set; }

    /// <summary>
    /// Filter the projects that are provisioned.
    /// </summary>
    [QueryString("onProvisionedOnly")]
    public bool? OnProvisinedOnly { get; set; }

    /// <summary>
    /// The key of the organization.
    /// </summary>
    [QueryString("organization")]
    public required string OrganizationKey { get; set; }

    /// <summary>
    /// Comma-separated list of project keys.
    /// </summary>
    [QueryString("projects")]
    public string? ProjectKeys { get; set; }


    /// <summary>
    /// Limit to:
    /// <list type="bullet">
    ///     <item><description>component names that contain the supplied string</description></item>
    ///     <item><description>component keys that contain the supplied string</description></item>
    /// </list>
    /// </summary>
    [QueryString("q")]
    public string? Query { get; set; }
}

public class CreateProjectsRequest
{
    /// <summary>
    /// The key of the organization
    /// </summary>
    [QueryString("organization")]
    public required string Organization { get; set; }

    /// <summary>
    /// Key of the project
    /// </summary>
    /// <example>
    /// my_project
    /// </example>
    [QueryString("project")]
    public required string Project { get; set; }

    /// <summary>
    /// Name of the project. If name is longer than 500, it is abbreviated.
    /// </summary>
    /// <example>
    /// SonarQube
    /// </example>
    [QueryString("name")]
    public required string Name { get; set; }

    [QueryString("newCodeDefinitionType")]
    public string? NewCodeDefinitionType { get; }

    [QueryString("newCodeDefinitionValue")]
    public string? NewCodeDefinitionValue { get; }

    /// <summary>
    /// Whether the created project should be visible to everyone, or only specific user/groups.
    /// If no visibility is specified, the default project visibility of the organization will be used.
    /// </summary>
    /// <example>private, public</example>
    [QueryString("visibility")]
    public string? Visibility { get; set; }
}
public class UpdateVisibilityRequest
{
    /// <summary>
    /// Project key
    /// </summary>
    [QueryString("project")]
    public required string Project { get; set; }

    /// <summary>
    /// Whether the project should be visible to everyone, or only specific user/groups.
    /// </summary>
    /// <example>private, public</example>
    [QueryString("visibility")]
    public required string Visibility { get; set; }
}

public class UpdateKeyRequest
{
    /// <summary>
    /// Current project key.
    /// </summary>
    [QueryString("from")]
    public required string From { get; set; }


    /// <summary>
    /// New project key.
    /// </summary>
    [QueryString("to")]
    public required string To { get; set; }
}

public class SearchProjectsResponse : Paged<Project>
{
}

public class DeleteProjectsRequest
{
    /// <summary>
    /// The project key to delete.
    /// </summary>
    [QueryString("project")]
    public required string Project { get; set; }
}

public class CreateProjectsResponse
{
    [JsonPropertyName("project")]
    public Project Project { get; set; } = Project.Empty;
}

public class SearchProjectsRequest
{
    /// <summary>
    /// Filter the projects for which last analysis is older than the given date (exclusive).
    /// Either a date(server timezone) or datetime can be provided.
    /// </summary>
    [QueryString("analyzedBefore")]
    public DateTime? AnalyzedBefore { get; set; }

    /// <summary>
    /// Filter the projects that are provisioned
    /// </summary>
    [QueryString("onProvisionedOnly")]
    public bool? OnProvisionedOnly { get; set; }

    /// <summary>
    /// The key of the organization
    /// </summary>
    [QueryString("organization")]
    public required string Organization { get; set; }

    /// <summary>
    /// 1-based page number
    /// </summary>
    [QueryString("p")]
    public int? Page { get; set; }

    /// <summary>
    /// Comma-separated list of project keys
    /// </summary>
    [QueryString("projects")]
    public string? Projects { get; set; }

    /// <summary>
    /// Page size. Must be greater than 0 and less or equal than 500
    /// </summary>
    [QueryString("ps")]
    public int? PageSize { get; set; }


    /// <summary>
    /// Limit search to:
    /// <list type="bullet">
    ///   <item>component names that contain the supplied string</item>
    ///   <item>component keys that contain the supplied string</item>
    /// </list>
    /// </summary>
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
        => client.Post($"{endpoint}/delete", request, token);

    public Task<SearchProjectsResponse> Search(SearchProjectsRequest request, CancellationToken token = default)
        => client.Get<SearchProjectsRequest, SearchProjectsResponse>($"{endpoint}/search", request, token);

    public Task UpdateKey(UpdateKeyRequest request, CancellationToken token = default)
        => client.Post($"{endpoint}/update_key", request, token);

    public Task UpdateVisibility(UpdateVisibilityRequest request, CancellationToken token = default)
        => client.Post($"{endpoint}/update_visibility", request, token);
}
