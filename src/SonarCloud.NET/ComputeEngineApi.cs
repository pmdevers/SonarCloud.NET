using SonarCloud.NET.Helpers;
using SonarCloud.NET.Models;
using SonarCloud.NET.Requests;

namespace SonarCloud.NET;

public interface IComputeEngineApi
{
    Task<SearchForTasksResponse> Search(SearchForTasksRequest request, CancellationToken token = default);
}
public record SearchForTasksResponse(Activity[] Tasks);

internal sealed class ComputeEngineApi(SonarCloudApiClient client) : IComputeEngineApi
{
    public const string Endpoint = "/api/ce";

    public async Task<SearchForTasksResponse> Search(SearchForTasksRequest request, CancellationToken token = default)
    {
        var response = await client.HttpClient.GetAsync($"{Endpoint}/activity" + QueryString.ToQueryString(request), token);
        var result = await client.HandleResponseAsync<SearchForTasksResponse>(response, token);
        return result;
    }

}
