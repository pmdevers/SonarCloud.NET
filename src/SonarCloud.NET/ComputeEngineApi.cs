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

    public Task<SearchForTasksResponse> Search(SearchForTasksRequest request, CancellationToken token = default)
        => client.Get<SearchForTasksRequest, SearchForTasksResponse>($"{Endpoint}/activity", request, token);
}
