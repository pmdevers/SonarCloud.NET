using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using SonarCloud.NET.Extensions;
using SonarCloud.NET.Helpers;
using System.Text.Json;

namespace SonarCloud.NET.Client;

public class SonarCloudApiClient(HttpClient client, SonarCloudApiClientOptions? options = null, ILogger<SonarCloudApiClient>? logger = null)
{
    public HttpClient HttpClient { get; } = client;
    public SonarCloudApiClientOptions Options { get; } = options ?? SonarCloudApiClientOptions.Default;
    public ILogger Logger { get; } = logger ?? NullLogger<SonarCloudApiClient>.Instance;

    internal async Task<T> HandleResponseAsync<T>(HttpResponseMessage response, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        HandleErrors(response);
        var result = await ReadContent<T>(response, cancellationToken);
        return result ?? throw Exceptions.EmptyResponse();
    }

    internal async Task<T?> ReadContent<T>(HttpResponseMessage response, CancellationToken cancellationToken = default)
    {
        var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        try
        {
            var result = await JsonSerializer.DeserializeAsync<T>(stream, Options.JsonOptions, cancellationToken);
            return result;
        } 
        catch(JsonException ex) 
        { 
            Logger.DeserializationError(ex.Message);
            throw;
        }
    }

    internal static void HandleErrors(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
            return;

        throw Exceptions.HttpErrorResponse(response.StatusCode, "Unknown error.");
    }
}
