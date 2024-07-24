using Microsoft.Extensions.DependencyInjection;

namespace SonarCloud.NET.Extensions;
public static class ServiceCollectionExtensions
{
    public static IHttpClientBuilder AddSonarCloudClient(this IServiceCollection services, Action<SonarCloudApiClientOptions> options)
    {
        var o = SonarCloudApiClientOptions.Default;
        options(o);

        services.AddSingleton(o);

        return services.AddHttpClient<ISonarCloudApiClient, SonarCloudApiClient>(builder =>
        {
            builder.BaseAddress = o.BaseAddress;
            builder.DefaultRequestHeaders.Add("Authorization", $"Bearer {o.AccessToken}");
        });
    }
}
