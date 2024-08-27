using SonarCloud.NET.Helpers;
using SonarCloud.NET.Models;

namespace SonarCloud.NET;

public interface IPermissionsApi
{
    /// <summary>
    /// Add permission to a group.
    /// This service defaults to global permissions, but can be limited to project permissions by providing project id or project key.
    /// The group name or group id must be provided.
    /// Requires the permission 'Administer' on the specified project.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task AddGroup(AddGroupRequest request, CancellationToken token);

    /// <summary>
    /// Add a group to a permission template.
    /// The group id or group name must be provided.
    /// Requires the permission 'Administer' on the organization.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task AddGroupToTemplate(AddGroupToTemplateRequest request, CancellationToken token);
    /// <summary>
    /// Add a project creator to a permission template.
    /// Requires the permission 'Administer' on the organization.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task AddProjectCreatorToTemplate(AddProjectCreatorToTemplateRequest request, CancellationToken token);

    /// <summary>
    /// Add permission to a user.
    /// This service defaults to global permissions, but can be limited to project permissions by providing project id or project key.
    /// Requires the permission 'Administer' on the specified project.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task AddUser(AddUserRequest request, CancellationToken token);
    /// <summary>
    /// Add a user to a permission template.
    /// Requires the permission 'Administer' on the organization.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task AddUserToTemplate(AddUserToTemplateRequest request, CancellationToken token);
    /// <summary>
    /// Apply a permission template to one project.
    /// The project id or project key must be provided.
    /// The template id or name must be provided.
    /// Requires the permission 'Administer' on the organization.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task ApplyTemplate(ApplyTemplateRequest request, CancellationToken token);
    /// <summary>
    /// Apply a permission template to several projects.
    /// The template id or name must be provided.
    /// Requires the permission 'Administer' on the organization.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task BulkApplyTemplate(BulkApplyTemplateRequest request, CancellationToken token);
    /// <summary>
    /// Create a permission template.
    /// Requires the permission 'Administer' on the organization.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<CreateTemplateResponse> CreateTemplate(CreateTemplateRequest request, CancellationToken token);

    /// <summary>
    /// Delete a permission template.
    /// Requires the permission 'Administer' on the organization.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task DeleteTemplate(DeleteTemplateRequest request, CancellationToken token);
    /// <summary>
    /// Remove a permission from a group.
    /// This service defaults to global permissions, but can be limited to project permissions by providing project id or project key.
    /// The group id or group name must be provided, not both.
    /// Requires the permission 'Administer' on the specified project.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task RemoveGroup(RemoveGroupRequest request, CancellationToken token);
    /// <summary>
    /// Add a group to a permission template.
    /// The group id or group name must be provided.
    /// Requires the permission 'Administer' on the organization.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task RemoveGroupFromTemplate(RemoveGroupFromTemplateRequest request, CancellationToken token);
    /// <summary>
    /// Remove a project creator from a permission template.
    /// Requires the permission 'Administer' on the organization.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task RemoveProjectCreatorFromTemplate(RemoveProjectCreatorFromTemplateRequest request, CancellationToken token);
    /// <summary>
    /// Remove permission from a user.
    /// This service defaults to global permissions, but can be limited to project permissions by providing project id or project key.
    /// Requires the permission 'Administer' on the specified project.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task RemoveUser(RemoveUserRequest request, CancellationToken token);

    /// <summary>
    /// Remove a user from a permission template.
    /// Requires the permission 'Administer' on the organization.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task RemoveUserFromTemplate(RemoveUserFromTemplateRequest request, CancellationToken token);
    /// <summary>
    /// List permission templates.
    /// Requires the permission 'Administer' on the organization.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<SearchTemplatesResponse> SearchTemplates(SearchTemplatesRequest request, CancellationToken token);

    /// <summary>
    /// Set a permission template as default.
    /// Requires the permission 'Administer' on the organization.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task SetDefaultTemplate(SetDefaultTemplateRequest request, CancellationToken token);

    /// <summary>
    /// Update a permission template.
    /// Requires the permission 'Administer' on the organization.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<UpdateTemplateResponse> UpdateTemplate(UpdateTemplateRequest request, CancellationToken token);
}

#pragma warning disable S2094 // Classes should not be empty
public class UpdateTemplateRequest
// Classes should not be empty
{
}

public class UpdateTemplateResponse
{
}

public class SetDefaultTemplateRequest
{
}

public class SearchTemplatesRequest
{
}

public class SearchTemplatesResponse
{
}

public class RemoveUserFromTemplateRequest
{
}

public class RemoveUserRequest
{
}

public class RemoveProjectCreatorFromTemplateRequest
{
}

public class RemoveGroupFromTemplateRequest
{
}

public class RemoveGroupRequest
{
}

public class DeleteTemplateRequest
{
}

public class CreateTemplateResponse
{
}

public class CreateTemplateRequest
{
}

public class BulkApplyTemplateRequest
{
}

#pragma warning restore S2094

/// <summary>
/// Apply Template Request.
/// </summary>
public class ApplyTemplateRequest
{
    /// <summary>
    /// Key of organization, used when group name is set.
    /// </summary>
    [QueryString("organization")]
    public string? Organization { get; set; }

    /// <summary>
    /// Project id.
    /// </summary>
    [QueryString("projectId")]
    public string? ProjectId { get; set; }

    /// <summary>
    /// Project key.
    /// </summary>
    [QueryString("projectKey")]
    public string? ProjectKey { get; set; }

    /// <summary>
    /// Template id
    /// </summary>
    [QueryString("templateId")]
    public string? TemplateId { get; set; }

    /// <summary>
    /// Template name
    /// </summary>
    [QueryString("templateName")]
    public string? TemplateName { get; set; }
}

/// <summary>
/// Add user to template request.
/// </summary>
public class AddUserToTemplateRequest
{
    /// <summary>
    /// User login.
    /// </summary>
    [QueryString("login")]
    public required string Login { get; set; }

    /// <summary>
    /// Key of organization, used when group name is set.
    /// </summary>
    [QueryString("organization")]
    public string? Organization { get; set; }

    /// <summary>
    /// Permission <br />
    /// Possible values for project permissions admin, codeviewer, issueadmin, securityhotspotadmin, scan, user
    /// </summary>
    [QueryString("permission")]
    public required string Permission { get; set; }

    /// <summary>
    /// Template id
    /// </summary>
    [QueryString("templateId")]
    public string? TemplateId { get; set; }

    /// <summary>
    /// Template name
    /// </summary>
    [QueryString("templateName")]
    public string? TemplateName { get; set; }
}


/// <summary>
/// Add user request.
/// </summary>
public class AddUserRequest
{
    /// <summary>
    /// User login.
    /// </summary>
    [QueryString("login")]
    public required string Login { get; set; }

    /// <summary>
    /// Key of organization, used when group name is set.
    /// </summary>
    [QueryString("organization")]
    public required string Organization { get; set; }

    /// <summary>
    /// Permission <br />
    /// Possible values for global permissions: admin, profileadmin, gateadmin, scan, provisioning <br />
    /// Possible values for project permissions admin, codeviewer, issueadmin, securityhotspotadmin, scan, user
    /// </summary>
    [QueryString("permission")]
    public required string Permission { get; set; }

    /// <summary>
    /// Project id.
    /// </summary>
    [QueryString("projectId")]
    public string? ProjectId { get; set; }

    /// <summary>
    /// Project name.
    /// </summary>
    [QueryString("projectKey")]
    public string? ProjectKey { get; set; }
}


/// <summary>
/// Add Project Creator To Template Request.
/// </summary>
public class AddProjectCreatorToTemplateRequest
{
    /// <summary>
    /// Key of organization, used when group name is set.
    /// </summary>
    [QueryString("organization")]
    public string? Organization { get; set; }

    /// <summary>
    /// Permission <br />
    /// Possible values for project permissions admin, codeviewer, issueadmin, securityhotspotadmin, scan, user
    /// </summary>
    [QueryString("permission")]
    public required string Permission { get; set; }

    /// <summary>
    /// Template id
    /// </summary>
    [QueryString("templateId")]
    public string? TemplateId { get; set; }

    /// <summary>
    /// Template name
    /// </summary>
    [QueryString("templateName")]
    public string? TemplateName { get; set; }
}


/// <summary>
/// Add Group To Template Request.
/// </summary>
public class AddGroupToTemplateRequest
{
    /// <summary>
    /// Group id
    /// </summary>
    [QueryString("groupId")]
    public string? GroupId { get; set; }

    /// <summary>
    /// Group name or 'anyone' (case insensitive)
    /// </summary>
    [QueryString("groupName")]
    public string? GroupName { get; set; }

    /// <summary>
    /// Key of organization, used when group name is set.
    /// </summary>
    [QueryString("organization")]
    public string? Organization { get; set; }

    /// <summary>
    /// Permission <br />
    /// Possible values for project permissions: admin, codeviewer, issueadmin, securityhotspotadmin, scan, user
    /// </summary>
    [QueryString("permission")]
    public required string Permission { get; set; }

    /// <summary>
    /// Template id
    /// </summary>
    [QueryString("templateId")]
    public string? TemplateId { get; set; }

    /// <summary>
    /// Template name
    /// </summary>
    [QueryString("templateName")]
    public string? TemplateName { get; set; }
}


/// <summary>
/// Add Group Request.
/// </summary>
public class AddGroupRequest
{
    /// <summary>
    /// Group id.
    /// </summary>
    [QueryString("groupId")]
    public string? GroupId { get; set; }

    /// <summary>
    /// Group name or 'anyone' (case insensitive)
    /// </summary>
    [QueryString("groupName")]
    public string? GroupName { get; set; }

    /// <summary>
    /// Key of organization, used when group name is set
    /// </summary>
    [QueryString("organization")]
    public string? Organization { get; set; }

    /// <summary>
    /// Permission
    /// <list type="bullet">
    /// <item><description>Possible values for global permissions: admin, profileadmin, gateadmin, scan, provisioning</description></item>
    /// <item><description>Possible values for project permissions admin, codeviewer, issueadmin, securityhotspotadmin, scan, user</description></item>
    /// </list>
    /// </summary>
    [QueryString("permission")]
    public required string Permission { get; set; }

    /// <summary>
    /// Project id
    /// </summary>
    [QueryString("projectId")]
    public string? ProjectId { get; set; }

    /// <summary>
    /// Project key
    /// </summary>
    [QueryString("projectKey")]
    public string? ProjectKey { get; set; }
}

internal class PermissionsApi(SonarCloudApiClient client) : IPermissionsApi
{
    private const string endpoint = "api/permissions";
    public Task AddGroup(AddGroupRequest request, CancellationToken token)
        => client.Post(endpoint + "/add_group", request, token);

    public Task AddGroupToTemplate(AddGroupToTemplateRequest request, CancellationToken token)
        => client.Post(endpoint + "/add_group_to_template", request, token);

    public Task AddProjectCreatorToTemplate(AddProjectCreatorToTemplateRequest request, CancellationToken token)
        => client.Post(endpoint + "/add_project_creator_to_template", request, token);

    public Task AddUser(AddUserRequest request, CancellationToken token)
        => client.Post(endpoint + "/add_user", request, token);

    public Task AddUserToTemplate(AddUserToTemplateRequest request, CancellationToken token)
        => client.Post(endpoint + "/add_user_to_template", request, token);

    public Task ApplyTemplate(ApplyTemplateRequest request, CancellationToken token)
        => client.Post(endpoint + "/apply_template", request, token);

    public Task BulkApplyTemplate(BulkApplyTemplateRequest request, CancellationToken token)
        => client.Post(endpoint + "/bulk_apply_template", request, token);

    public Task<CreateTemplateResponse> CreateTemplate(CreateTemplateRequest request, CancellationToken token)
        => client.Post<CreateTemplateRequest, CreateTemplateResponse>(endpoint + "/create_template", request, token);

    public Task DeleteTemplate(DeleteTemplateRequest request, CancellationToken token)
         => client.Post(endpoint + "/delete_template", request, token);

    public Task RemoveGroup(RemoveGroupRequest request, CancellationToken token)
         => client.Post(endpoint + "/remove_group", request, token);

    public Task RemoveGroupFromTemplate(RemoveGroupFromTemplateRequest request, CancellationToken token)
         => client.Post(endpoint + "/remove_group_from_template", request, token);

    public Task RemoveProjectCreatorFromTemplate(RemoveProjectCreatorFromTemplateRequest request, CancellationToken token)
         => client.Post(endpoint + "/remove_project_creator_from_template", request, token);

    public Task RemoveUser(RemoveUserRequest request, CancellationToken token)
         => client.Post(endpoint + "/remove_user", request, token);

    public Task RemoveUserFromTemplate(RemoveUserFromTemplateRequest request, CancellationToken token)
        => client.Post(endpoint + "/remove_user_from_template", request, token);

    public Task<SearchTemplatesResponse> SearchTemplates(SearchTemplatesRequest request, CancellationToken token)
         => client.Post<SearchTemplatesRequest, SearchTemplatesResponse>(endpoint + "/search_templates", request, token);

    public Task SetDefaultTemplate(SetDefaultTemplateRequest request, CancellationToken token)
         => client.Post(endpoint + "/set_default_template", request, token);

    public Task<UpdateTemplateResponse> UpdateTemplate(UpdateTemplateRequest request, CancellationToken token)
         => client.Post<UpdateTemplateRequest, UpdateTemplateResponse>(endpoint + "/update_template", request, token);
}
