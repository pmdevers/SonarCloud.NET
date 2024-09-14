using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Net;
using System.Runtime.CompilerServices;

namespace SonarCloud.NET.Tests.Helper;
public static class SnapshotReader<T>
{
    public static HttpResponseMessage Read(HttpStatusCode statusCode, string method)
    {
        var result = Read($"{statusCode}_{method}");
        return new() {
            StatusCode = statusCode,
            Content = new StringContent(result)
        };
    }

    public static string Read(string name)
    {
        var resourceName = $"SonarCloud.NET.Tests.Responses.{typeof(T).Name}.{name}.json";
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
        if (stream == null)
        {
            return string.Empty;
        }

        using var reader = new StreamReader(stream);
        var json = reader.ReadToEnd();
        return json;
    }
}
