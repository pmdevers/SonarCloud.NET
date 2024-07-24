using System.Text.Json.Serialization;

namespace SonarCloud.NET;

public interface IAuthenticationApi
{
    Task Logout(CancellationToken token = default);
    Task<bool> Validate(CancellationToken token = default);
}

internal sealed class AuthenticationApi(SonarCloudApiClient client) : IAuthenticationApi
{
    private const string endpoint = "api/authentication";

    public async Task Logout(CancellationToken token = default)
    {
        var response = await client.HttpClient.PostAsync($"{endpoint}/logout", null, token)!;
        SonarCloudApiClient.HandleErrors(response);
    }

    public async Task<bool> Validate(CancellationToken token = default)
    {
        var response = await client.HttpClient.GetAsync($"{endpoint}/validate", token);
        var result = await client.HandleResponseAsync<ValidateResponse>(response, token);
        return result.Valid;
    }

    private sealed class ValidateResponse
    {
        [JsonPropertyName("valid")]
        public bool Valid { get; set; }
    }
}


