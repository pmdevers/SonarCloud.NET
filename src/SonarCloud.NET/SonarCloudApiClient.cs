using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using SonarCloud.NET.Apis;
using SonarCloud.NET.Extensions;
using SonarCloud.NET.Helpers;
using System.Text.Json;

namespace SonarCloud.NET;

public interface ISonarCloudApiClient
{
    IAuthenticationApi Authentication { get; }
    IComputeEngineApi ComputeEngine { get; }
    IComponentsApi Components { get; }
    IDuplicationsApi Duplications { get; }
    IFavoritesApi Favorites { get; }
    IHotspotsApi Hotspots { get; }

    IIssuesApi Issues { get; }
    IProjectTagsApi ProjectTags { get; }
    IProjectsApi Projects { get; }
    IPermissionsApi Permissions { get; }

    IWebServicesApi WebServices { get; }
}

internal class SonarCloudApiClient(HttpClient client, SonarCloudApiClientOptions? options = null, ILogger<SonarCloudApiClient>? logger = null)
    : ISonarCloudApiClient
{
    public HttpClient HttpClient { get; } = client;
    public SonarCloudApiClientOptions Options { get; } = options ?? SonarCloudApiClientOptions.Default;
    public ILogger Logger { get; } = logger ?? NullLogger<SonarCloudApiClient>.Instance;
    public IAuthenticationApi Authentication => new AuthenticationApi(this);
    public IComputeEngineApi ComputeEngine => new ComputeEngineApi(this);
    public IComponentsApi Components => new ComponentsApi(this);
    public IDuplicationsApi Duplications => new DuplicationsApi(this);
    public IFavoritesApi Favorites => new FavoritesApi(this);
    public IHotspotsApi Hotspots => new HotspotsApi(this);
    public IIssuesApi Issues => new IssuesApi(this);
    public IProjectTagsApi ProjectTags => new ProjectTagsApi(this);
    public IProjectsApi Projects => new ProjectsApi(this);
    public IPermissionsApi Permissions => new PermissionsApi(this);
    public IWebServicesApi WebServices => new WebServicesApi(this);

    public async Task<TResponse> Post<TRequest, TResponse>(string url, TRequest request, CancellationToken token = default)
    {
        var content = new FormUrlEncodedContent(QueryString.ToNameValueCollection(request));
        var response = await HttpClient.PostAsync(url, content, token)!;
        return await HandleResponseAsync<TResponse>(response, token);
    }

    public async Task Post<TRequest>(string url, TRequest request, CancellationToken cancellationToken = default)
    {
        var content = new FormUrlEncodedContent(QueryString.ToNameValueCollection(request));
        var response = await HttpClient.PostAsync(url, content, cancellationToken);
        await HandleErrors(response, cancellationToken);
    }

    public async Task Post(string url, CancellationToken cancellationToken = default)
    {
        var response = await HttpClient.PostAsync(url, null, cancellationToken);
        await HandleErrors(response, cancellationToken);
    }

    public async Task<TResponse> Get<TRequest, TResponse>(string url, TRequest request, CancellationToken cancellationToken = default)
    {
        var response = await HttpClient.GetAsync(url + QueryString.AsQueryString(request), cancellationToken);
        return await HandleResponseAsync<TResponse>(response, cancellationToken);
    }

    public async Task<TResponse> Get<TResponse>(string url, CancellationToken cancellationToken = default)
    {
        var response = await HttpClient.GetAsync(url, cancellationToken);
        return await HandleResponseAsync<TResponse>(response, cancellationToken);
    }

    internal async Task<T> HandleResponseAsync<T>(HttpResponseMessage response, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await HandleErrors(response, cancellationToken);
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
        catch (JsonException ex)
        {
            Logger.DeserializationError(ex.Message);
            throw;
        }
    }
    internal static async Task HandleErrors(HttpResponseMessage response, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (response.IsSuccessStatusCode)
            return;

        var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        using var sr = new StreamReader(stream);
        var message = await sr.ReadToEndAsync(cancellationToken);

        throw Exceptions.HttpErrorResponse(response.StatusCode, message);
    }
}
