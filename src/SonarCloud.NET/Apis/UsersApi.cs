using SonarCloud.NET.Helpers;
using SonarCloud.NET.Models;

namespace SonarCloud.NET.Apis;

/// <summary>
/// Manage users.
/// </summary>
public interface IUsersApi
{
    /// <summary>
    /// Lists the groups a user belongs to.
    /// Requires the permission 'Administer' on the organization.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<UsersGroupsResponse> Groups(UsersGroupsRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a list of active users from organizations the user making the request belongs to.<br />
    /// The following fields are only returned for the logged-in user :
    /// <list type="bullet">
    ///    <item>email</item>
    ///    <item>externalIdentity</item>
    ///    <item>externalProvider</item>
    ///    <item>groups</item>
    ///    <item>lastConnectionDate</item>
    ///    <item>tokensCount</item>
    /// </list>
    /// <b>Field 'lastConnectionDate' is only updated every hour, so it may not be accurate, for instance when a user authenticates many times in less than one hour.</b>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<UsersSearchResponse> Search(UsersSearchRequest request, CancellationToken cancellationToken = default);


}

public class UsersSearchResponse
{
    public required Paging Paging { get; set; }
    public required User[] Users { get; set; }
}

public class UsersSearchRequest
{
    /// <summary>
    /// Filter on a list of one or more (max 30) user identifiers (comma-separated UUID V4)
    /// </summary>
    [QueryString("ids")]
    public string? Ids { get; set; }

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
    /// Filter on login, name and email
    /// </summary>
    [QueryString("q")]
    public string? Query { get; set; }
}

public class UsersGroupsResponse
{
    public required Paging Paging { get; set; }
    public required Group[] Groups { get; set; }
}

public class UsersGroupsRequest
{
    /// <summary>
    /// A user login
    /// </summary>
    [QueryString("login")]
    public required string Login { get;set; }

    /// <summary>
    /// Organization key
    /// </summary>
    [QueryString("organization")]
    public required string Organization { get;set; }

    /// <summary>
    /// 1-based page number
    /// </summary>
    [QueryString("p")]
    public int? PageNumber { get;set; }

    /// <summary>
    /// Page size. Must be greater than 0.
    /// </summary>
    [QueryString("ps")]
    public int? PageSize { get;set; }

    /// <summary>
    /// Limit search to group names that contain the supplied string.
    /// </summary>
    [QueryString("q")]
    public string? Query { get;set;}

    /// <summary>
    /// Depending on the value, <br />
    /// show only selected items (selected=selected), <br />
    /// deselected items (selected=deselected), <br />
    /// or all items with their selection status (selected=all).
    /// </summary>
    [QueryString("selected")]
    public string? Selected { get;set; }
}


internal sealed class UsersApi(SonarCloudApiClient client) : IUsersApi
{
    private const string Endpiont = "api/users";

    public Task<UsersGroupsResponse> Groups(UsersGroupsRequest request, CancellationToken cancellationToken = default)
        => client.Get<UsersGroupsRequest, UsersGroupsResponse>($"{Endpiont}/groups", request, cancellationToken);

    public Task<UsersSearchResponse> Search(UsersSearchRequest request, CancellationToken cancellationToken = default)
        => client.Get<UsersSearchRequest, UsersSearchResponse>($"{Endpiont}/search", request, cancellationToken);
}
