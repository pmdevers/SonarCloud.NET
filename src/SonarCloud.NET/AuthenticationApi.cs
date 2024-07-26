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

    public Task Logout(CancellationToken token = default)
        => client.Post($"{endpoint}/logout", token);

    public async Task<bool> Validate(CancellationToken token = default)
        => (await client.Get<ValidateResponse>($"{endpoint}/validate", token)).Valid;
    private sealed class ValidateResponse
    {
        [JsonPropertyName("valid")]
        public bool Valid { get; set; }
    }
}


