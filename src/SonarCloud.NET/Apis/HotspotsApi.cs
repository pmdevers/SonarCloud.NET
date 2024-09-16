using SonarCloud.NET.Helpers;
using SonarCloud.NET.Models;
using System.Text.Json.Serialization;

namespace SonarCloud.NET.Apis;

public interface IHotspotsApi
{
    Task ChangeStatus(HotspotsChangeStatusRequest request, CancellationToken cancellationToken = default);
    Task<HotspotsSearchResponse> Search(HotspotsSearchRequest request, CancellationToken cancellationToken = default);
    Task<HotspotsShowResponse> Show(HotspotsShowRequest request, CancellationToken cancellationToken = default);
}

public class HotspotsChangeStatusRequest
{
    [QueryString("comment")]
    public string? Comment { get; set; }
    [QueryString("hotspot")]
    public required string Hotspot { get; set; }
    [QueryString("resolution")]
    public string? Resolution { get; set; }

    [QueryString("status")]
    public required string Status { get; set; }
}

public class HotspotsSearchResponse
{
    [JsonPropertyName("paging")]
    public Paging Paging { get; set; } = new();

    [JsonPropertyName("hotspots")]
    public Hotspot[] Hotspots { get; set; } = [];

    [JsonPropertyName("components")]
    public Component[] Components { get; set; } = [];
}

public class HotspotsSearchRequest
{
    [QueryString("fileUuids")]
    public string? FileUuids { get; set; }

    [QueryString("files")]
    public string? Files { get; set; }

    [QueryString("hotspots")]
    public string? Hotspots { get; set; }

    [QueryString("onlyMine")]
    public bool? OnlyMine { get; set; }

    [QueryString("p")]
    public int? Page { get; set; }

    [QueryString("projectKey")]
    public string? ProjectKey { get; set; }

    [QueryString("ps")]
    public int? PageSize { get; set; }

    [QueryString("resolution")]
    public string? Resolution { get; set; }

    [QueryString("sinceLeakPeriod")]
    public string? SinceLeakPeriod { get; set; }

    [QueryString("status")]
    public string? Status { get; set; }
}

public class HotspotsShowRequest
{
    [QueryString("hotspot")]
    public required string HotspotKey { get; set; }
}

public class HotspotsShowResponse
{
    [JsonPropertyName("key")]
    public required string Key { get; set; }
    [JsonPropertyName("component")]
    public required Component Component { get; set; }
    [JsonPropertyName("project")]
    public required Project Project { get; set; }
    [JsonPropertyName("rule")]
    public required Rule Rule { get; set; }
    [JsonPropertyName("status")]
    public required string Status { get; set; }
    [JsonPropertyName("line")]
    public int Line { get; set; }
    [JsonPropertyName("message")]
    public required string Message { get; set; }
    [JsonPropertyName("assignee")]
    public required string Assignee { get; set; }
    [JsonPropertyName("author")]
    public required string Author { get; set; }
    [JsonPropertyName("creationDate")]
    public DateTime CreationDate { get; set; }
    [JsonPropertyName("updateDate")]
    public DateTime UpdateDate { get; set; }
    [JsonPropertyName("changelog")]
    public Changelog[] Changelog { get; set; } = [];
    [JsonPropertyName("comment")]
    public Comment[] Comment { get; set; } = [];
    [JsonPropertyName("users")]
    public User[] Users { get; set; } = [];
    [JsonPropertyName("canChangeStatus")]
    public bool CanChangeStatus { get; set; }
}

internal class HotspotsApi(SonarCloudApiClient client) : IHotspotsApi
{
    private const string endpoint = "api/hotspots";
    public Task ChangeStatus(HotspotsChangeStatusRequest request, CancellationToken cancellationToken = default)
        => client.Post(endpoint + "/change_status", request, cancellationToken);

    public Task<HotspotsSearchResponse> Search(HotspotsSearchRequest request, CancellationToken cancellationToken = default)
        => client.Get<HotspotsSearchRequest, HotspotsSearchResponse>(endpoint + "/search", request, cancellationToken);

    public Task<HotspotsShowResponse> Show(HotspotsShowRequest request, CancellationToken cancellationToken = default)
        => client.Get<HotspotsShowRequest, HotspotsShowResponse>(endpoint + "/show", request, cancellationToken);
}
