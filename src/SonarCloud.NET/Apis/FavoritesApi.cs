using SonarCloud.NET.Helpers;
using SonarCloud.NET.Models;
using System.Text.Json.Serialization;

namespace SonarCloud.NET.Apis;

public interface IFavoritesApi
{
    Task Add(FavoritesAddRequest request, CancellationToken cancellationToken = default);
    Task Remove(FavoritesRemoveRequest request, CancellationToken cancellationToken = default);
    Task<FavoritesSearchResponse> Search(FavoritesSearchRequest request, CancellationToken cancellationToken = default);
}

public class FavoritesAddRequest
{
    [QueryString("component")]
    public required string ComponentKey { get; set; }
}

public class FavoritesRemoveRequest
{
    [QueryString("component")]
    public required string ComponentKey { get; set; }
}

public class FavoritesSearchResponse
{
    [JsonPropertyName("paging")]
    public Paging Paging { get; set; } = new();

    [JsonPropertyName("favorites")]
    public Favorite[] Favorites { get; set; } = [];

}

public class FavoritesSearchRequest
{
    [QueryString("p")]
    public int? Page { get; set; }
    [QueryString("ps")]
    public int? PageSize { get; set; }
}

internal class FavoritesApi(SonarCloudApiClient client) : IFavoritesApi
{
    private const string endpoint = "api/favorites";

    public Task Add(FavoritesAddRequest request, CancellationToken cancellationToken = default)
       => client.Post(endpoint + "/add", request, cancellationToken);

    public Task<FavoritesSearchResponse> Search(FavoritesSearchRequest request, CancellationToken cancellationToken = default)
          => client.Post<FavoritesSearchRequest, FavoritesSearchResponse>(endpoint + "/search", request, cancellationToken);
    public Task Remove(FavoritesRemoveRequest request, CancellationToken cancellationToken = default)
        => client.Post(endpoint + "/add", request, cancellationToken);

}
