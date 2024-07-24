using SonarCloud.NET.Client;
using System.Text.Json.Serialization;

namespace SonarCloud.NET;

public static class AuthenticationApi
{
    private const string endpoint = "api/authentication";

    public static async Task Logout(this SonarCloudApiClient client, CancellationToken token = default)
    {
        var response = await client.HttpClient.PostAsync($"{endpoint}/logout", null, token)!;
        SonarCloudApiClient.HandleErrors(response);
    }

    public static async Task<bool> Validate(this SonarCloudApiClient client, CancellationToken token = default)
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


