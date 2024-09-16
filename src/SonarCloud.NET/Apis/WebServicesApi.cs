using SonarCloud.NET.Helpers;
using System.Text.Json.Serialization;

namespace SonarCloud.NET.Apis;
public interface IWebServicesApi
{
    Task<WebServicesListResponse> List(CancellationToken cancellationToken = default);
    Task<WebServicesExampleResponse> ResponseExample(WebServicesExampleRequest request, CancellationToken cancellationToken = default);
}

public class WebServicesListResponse
{
    [JsonPropertyName("webServices")]
    public Endpoint[] WebServices { get; set; } = [];
}

public class Endpoint
{
    [JsonPropertyName("path")]
    public required string Path { get; set; }

    [JsonPropertyName("description")]
    public required string Description { get; set; }

    [JsonPropertyName("actions")]
    public EndpointAction[] Actions { get; set; } = [];
}

public class EndpointAction
{
    [JsonPropertyName("key")]
    public required string Key { get; init; }
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    [JsonPropertyName("deprecatedSince")]
    public string? DeprecatedSince { get; set; }
    [JsonPropertyName("internal")]
    public bool Internal { get; set; }
    [JsonPropertyName("post")]
    public bool Post { get; set; }
    [JsonPropertyName("hasResponseExample")]
    public bool HasResponseExample { get; set; }
    [JsonPropertyName("changelog")]
    public ChangelogItem[] Changelog { get; set; } = [];
    [JsonPropertyName("params")]
    public EndpointActionParameter[] Params { get; set; } = [];

}

public class EndpointActionParameter
{
    [JsonPropertyName("key")]
    public required string Key { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("required")]
    public bool Required { get; set; }
    [JsonPropertyName("internal")]
    public bool Internal { get; set; }

    [JsonPropertyName("exampleValue")]
    public string? ExampleValue { get; set; }

    [JsonExtensionData]
    public Dictionary<string, object> Values { get; set; } = [];
}

public class ChangelogItem
{
    [JsonPropertyName("description")]
    public required string Description { get; set; }
    [JsonPropertyName("version")]
    public required string Version { get; set; }
}


public class WebServicesExampleRequest
{
    [QueryString("action")]
    public required string Action { get; set; }
    [QueryString("controller")]
    public required string Controller { get; set; }
}
public class WebServicesExampleResponse
{
    [JsonPropertyName("format")]
    public required string Format { get; set; }
    [JsonPropertyName("example")]
    public required string Example { get; set; }
}


internal sealed class WebServicesApi(SonarCloudApiClient client) : IWebServicesApi
{
    private const string endpoint = "api/webservices";
    public Task<WebServicesListResponse> List(CancellationToken cancellationToken = default)
        => client.Get<WebServicesListResponse>(endpoint + "/list", cancellationToken);

    public Task<WebServicesExampleResponse> ResponseExample(WebServicesExampleRequest request, CancellationToken cancellationToken = default)
        => client.Get<WebServicesExampleRequest, WebServicesExampleResponse>(endpoint + "/response_example", request, cancellationToken);
}
