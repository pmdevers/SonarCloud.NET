using System.Net;
using SonarCloud.NET.Client;

namespace SonarCloud.NET.Helpers;
internal static class Exceptions
{
    public static SonarCloudApiClientException HttpErrorResponse(HttpStatusCode statusCode, string message)
        => new($"HttpErrorCode: '{statusCode}', Reason: {message}.");

    internal static SonarCloudApiClientException EmptyResponse()
        => new("Deserialize result was empty");
}
