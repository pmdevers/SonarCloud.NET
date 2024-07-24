using Microsoft.Extensions.DependencyInjection;
using SonarCloud.NET.Client;

namespace SonarCloud.NET.Extensions;
public static class ServiceCollectionExtensions
{
    public static IHttpClientBuilder AddSonarCloudClient(this IServiceCollection services, Action<SonarCloudApiClientOptions> options)
    {
        var o = SonarCloudApiClientOptions.Default;
        options(o);

        services.AddSingleton(o);

        return services.AddHttpClient<SonarCloudApiClient>(builder =>
        {
            builder.BaseAddress = o.BaseAddress;
            builder.DefaultRequestHeaders.Add("Authorization", $"Bearer {o.AccessToken}");
        });
    }
}
