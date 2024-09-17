using SonarCloud.NET.Helpers;
using SonarCloud.NET.Models;
using System.Text.Json.Serialization;

namespace SonarCloud.NET.Apis;

public interface IUserGroupsApi
{
    /// <summary>
    /// Add a user to a group.<br />
    /// 'id' or 'name' must be provided.<br />
    /// <br /> <b>Requires the following permission: 'Administer System'.</b>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    Task AddUser(UserGroupsAddUserRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a group.<br />
    /// <b>Requires the following permission: 'Administer System'.</b>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<UserGroupsCreateResponse> Create(UserGroupsCreateRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a group. The default groups cannot be deleted.<br />
    /// <b>Requires the following permission: 'Administer System'.</b>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task Delete(UserGroupsDeleteRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove a user from a group. <br/>
    /// <b>Requires the following permission: 'Administer System'.</b>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task RemoveUser(UserGroupsRemoveUserRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Search for user groups. <br />
    /// <b>Requires the following permission: 'Administer System'.</b>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<UserGroupsSearchResponse> Search(UserGroupsSearchRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a group. <br />
    /// <b>Requires the following permission: 'Administer System'.</b>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task Update(UserGroupsUpdateRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Search for users with membership information with respect to a group. <br />
    /// <b>Requires the following permission: 'Administer System'.</b>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<UserGroupsUsersResponse> Users(UserGroupsUsersRequest request, CancellationToken cancellationToken = default);
}

public class UserGroupsUsersResponse
{
    [JsonPropertyName("p")]
    public int Index { get; set; } = 0;
    [JsonPropertyName("ps")]
    public int Size { get; set; } = 0;
    [JsonPropertyName("total")]
    public int Total { get; set; } = 0;

    [JsonPropertyName("users")]
    public required UserGroupsUsersResponseUser[] Users { get; set; }
}

public class UserGroupsUsersResponseUser
{
    [JsonPropertyName("login")]
    public required string Login { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("selected")]
    public required bool Selected { get; set;}
}

public class UserGroupsUsersRequest
{
    /// <summary>
    /// Group id
    /// </summary>
    [QueryString("id")]
    public int? Id { get; set; }

    /// <summary>
    /// Group name
    /// </summary>
    [QueryString("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Key of organization
    /// </summary>
    [QueryString("organization")]
    public required string Organization { get; set; }

    /// <summary>
    /// 1-based page number
    /// </summary>
    [QueryString("p")]
    public int? PageNumber { get; set; }

    /// <summary>
    /// Page size. Must be greater than 0 and less or equal than 500
    /// </summary>
    [QueryString("ps")]
    public int? PageSize { get; set; }

    /// <summary>
    /// Limit search to names that contain the supplied string.
    /// </summary>
    [QueryString("q")]
    public string? Query { get; set; }

    /// <summary>
    /// Depending on the value, <br />
    /// show only selected items (selected=selected),<br />
    /// deselected items (selected=deselected), <br />
    /// or all items with their selection status (selected=all).<br />
    /// </summary>
    [QueryString("selected")]
    public string? Selected { get; set; }
}

public class UserGroupsUpdateRequest
{
    /// <summary>
    /// New optional description for the group. <br />
    /// A group description cannot be larger than 200 characters. <br />
    /// <i>If value is not defined, then description is not changed.</i>
    /// </summary>
    [QueryString("description")]
    public string? Description { get;set; }

    /// <summary>
    /// Identifier of the group.
    /// </summary>
    [QueryString("id")]
    public int Id { get; set; }

    /// <summary>
    /// New optional name for the group. <br />
    /// A group name cannot be larger than 255 characters and must be unique. <br />
    /// <b>Value 'anyone' (whatever the case) is reserved and cannot be used. </b> <br />
    /// <i>If value is empty or not defined, then name is not changed.</i>
    /// </summary>
    [QueryString("name")]
    public string? Name { get; set; }
}

public class UserGroupsSearchResponse
{
    public required Paging Paging { get; set; }
    public required Group[] Groups { get; set; }
}

public class UserGroupsSearchRequest
{
    /// <summary>
    /// Comma-separated list of the fields to be returned in response. <br />
    /// All the fields are returned by default.
    /// </summary>
    [QueryString("f")]
    public string? Fields { get; set; }

    /// <summary>
    /// Key of organization
    /// </summary>
    [QueryString("organization")]
    public required string Organization { get;set; }

    /// <summary>
    /// 1-based page number
    /// </summary>
    [QueryString("p")]
    public int? PageNumber { get; set; }

    /// <summary>
    /// Page size. Must be greater than 0 and less or equal than 500
    /// </summary>
    [QueryString("ps")]
    public int? PageSize { get; set; }

    /// <summary>
    /// Limit search to names that contain the supplied string.
    /// </summary>
    [QueryString("q")]
    public string? Query { get; set; }

}

public class UserGroupsRemoveUserRequest
{
    /// <summary>
    /// Group id
    /// </summary>
    [QueryString("id")]
    public int? Id { get;set; }

    /// <summary>
    /// User login
    /// </summary>
    [QueryString("login")]
    public string? Login { get; set; }

    /// <summary>
    /// Group name
    /// </summary>
    [QueryString("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Key of organization
    /// </summary>
    [QueryString("organization")]
    public string? Organization { get;set; }
}

public class UserGroupsDeleteRequest
{
    /// <summary>
    /// Group id
    /// </summary>
    [QueryString("id")]
    public int? Id { get; set; }

    /// <summary>
    /// Group name
    /// </summary>
    [QueryString("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Key of organization
    /// </summary>
    [QueryString("organization")]
    public string? Organization { get; set; }
}

public class UserGroupsCreateResponse
{
    [JsonPropertyName("group")]
    public required Group Group { get; set; }
}

public class UserGroupsCreateRequest
{
    /// <summary>
    /// Description for the new group. <br />
    /// A group description cannot be larger than 200 characters.
    /// </summary>
    [QueryString("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Name for the new group.  <br />
    /// A group name cannot be larger than 255 characters and must be unique. <br />
    /// <b>The value 'anyone' (whatever the case) is reserved and cannot be used.</b>
    /// </summary>
    [QueryString("name")]
    public required string Name { get;set; }

    /// <summary>
    /// Key of organization
    /// </summary>
    [QueryString("organization")]
    public required string Organinzation { get; set; }
}

public class UserGroupsAddUserRequest
{
    /// <summary>
    /// Group id
    /// </summary>
    [QueryString("id")]
    public int? Id { get; set; }

    /// <summary>
    /// User login
    /// </summary>
    [QueryString("login")]
    public string? Login { get; set; }

    /// <summary>
    /// Group name
    /// </summary>
    [QueryString("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Key of organization
    /// </summary>
    [QueryString("organization")]
    public string? Organization { get; set; }
}



internal sealed class UserGroupsApi(SonarCloudApiClient client) : IUserGroupsApi
{
    private const string Endpoint = "api/user_groups";

    public Task AddUser(UserGroupsAddUserRequest request, CancellationToken cancellationToken = default)
        => client.Post($"{Endpoint}/add_user", request, cancellationToken);

    
    public Task<UserGroupsCreateResponse> Create(UserGroupsCreateRequest request, CancellationToken cancellationToken = default)
        => client.Post<UserGroupsCreateRequest, UserGroupsCreateResponse>($"{Endpoint}/create", request, cancellationToken);

    public Task Delete(UserGroupsDeleteRequest request, CancellationToken cancellationToken = default)
        => client.Post($"{Endpoint}/delete", request,cancellationToken);

    public Task RemoveUser(UserGroupsRemoveUserRequest request, CancellationToken cancellationToken = default)
        => client.Post($"{Endpoint}/remove_user", request, cancellationToken);

    public Task<UserGroupsSearchResponse> Search(UserGroupsSearchRequest request, CancellationToken cancellationToken = default)
        => client.Get<UserGroupsSearchRequest, UserGroupsSearchResponse>($"{Endpoint}/search", request, cancellationToken);

    public Task Update(UserGroupsUpdateRequest request, CancellationToken cancellationToken = default)
        => client.Post($"{Endpoint}/update", request, cancellationToken);

    public Task<UserGroupsUsersResponse> Users(UserGroupsUsersRequest request, CancellationToken cancellationToken = default)
        => client.Get<UserGroupsUsersRequest, UserGroupsUsersResponse>($"{Endpoint}/users", request, cancellationToken);
}
