using Microsoft.Extensions.DependencyInjection;
using SonarCloud.NET.Extensions;
using System.Net;
using System.Text;
using System.Text.Json;

namespace SonarCloud.NET.Tests.Helper;
public class TestsHelper
{
    private static readonly JsonSerializerOptions options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public static ISonarCloudApiClient GetClient(HttpResponseMessage response)
    {
        var services = new ServiceCollection();

        services.AddSonarCloudClient(opt =>
        {
            opt.AccessToken = "TEST";
            
        }).AddHttpMessageHandler(() => new FakeHttpMessageHandler(response));

        var provider = services.BuildServiceProvider();

        return provider.GetRequiredService<ISonarCloudApiClient>();
    }

    public static ISonarCloudApiClient GetClient(object responseObject, HttpStatusCode statusCode)
    {
        var jsonContent = JsonSerializer.Serialize(responseObject, options);
        var response = new HttpResponseMessage()
        {
            StatusCode = statusCode,
            Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
        };

        return GetClient(response);
    }
}
