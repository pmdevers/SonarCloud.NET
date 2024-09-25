using SonarCloud.NET.Helpers;
using SonarCloud.NET.Models;
using System.Text.Json.Serialization;



namespace SonarCloud.NET.Apis;

/// <summary>
/// List, create, and delete a user's access tokens.
/// </summary>
public interface IUserTokensApi
{
    /// <summary>
    /// Generate a user access token.
    /// Please keep your tokens secret.They enable to authenticate and analyze projects.
    /// It requires administration permissions to specify a 'login' and generate a token for another user.Otherwise, a token is generated for the current user.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<UserTokensGenerateResponse> Generate(UserTokensGenerateRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Revoke a user access token.
    /// It requires administration permissions to specify a 'login' and revoke a token for another user.Otherwise, the token for the current user is revoked.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task Revoke(UserTokensRevokeRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// List the access tokens of a user.
    /// The login must exist and active.
    /// <b>Field 'lastConnectionDate' is only updated every hour, so it may not be accurate, for instance when a user is using a token many times in less than one hour.</b>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<UserTokensSearchResponse> Search(UserTokensSearchRequest request, CancellationToken cancellationToken = default);
}

public class UserTokensSearchResponse
{
    [JsonPropertyName("login")]
    public required string Login{ get; set; }

    [JsonPropertyName("userTokens")]
    public required UserToken[] UserTokens { get; set; }
 }

public class UserTokensSearchRequest
{
    [QueryString("login")]
    public string? Login { get; set; }
}

public class UserTokensRevokeRequest
{
    [QueryString("login")]
    public string? Login { get; set; }

    [QueryString("name")]
    public required string Name { get; set; }
}

public class UserTokensGenerateResponse
{
    [JsonPropertyName("login")]
    public required string Login { get; set; }
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }
    [JsonPropertyName("token")]
    public required string Token { get; set; }
}

public class UserTokensGenerateRequest
{
    /// <summary>
    /// User login. If not set, the token is generated for the authenticated user.
    /// </summary>
    [QueryString("login")]
    public string? Login { get; set;}

    /// <summary>
    /// Token name
    /// </summary>
    [QueryString("name")]
    public required string Name { get; set; }
}


internal sealed class UserTokensApi(SonarCloudApiClient client) : IUserTokensApi
{
    private const string Endpoint = "api/user_tokens";

    public Task<UserTokensGenerateResponse> Generate(UserTokensGenerateRequest request, CancellationToken cancellationToken = default)
        => client.Post<UserTokensGenerateRequest, UserTokensGenerateResponse>($"{Endpoint}/generate", request, cancellationToken);

    public Task Revoke(UserTokensRevokeRequest request, CancellationToken cancellationToken = default)
        => client.Post($"{Endpoint}/revoke", request, cancellationToken);

    public Task<UserTokensSearchResponse> Search(UserTokensSearchRequest request, CancellationToken cancellationToken = default)
        => client.Get<UserTokensSearchRequest, UserTokensSearchResponse>($"{Endpoint}/search", request, cancellationToken);
}
