using SonarCloud.NET.Client;
using SonarCloud.NET.Helpers;
using SonarCloud.NET.Models;
using SonarCloud.NET.Requests;

namespace SonarCloud.NET;

public static class ComputeEngineApi
{
    public const string Endpoint = "/api/ce";

    public record SearchForTasksResponse(Activity[] Tasks);
    public static async Task<SearchForTasksResponse> Search(this SonarCloudApiClient client, SearchForTasksRequest request, CancellationToken token = default)
    {
        var response = await client.HttpClient.GetAsync($"{Endpoint}/activity" + QueryString.ToQueryString(request), token);
        var result = await client.HandleResponseAsync<SearchForTasksResponse>(response, token);
        return result;
    }

}
