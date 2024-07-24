using System.Net;

namespace SonarCloud.NET;
public class SonarCloudApiClientException : Exception
{
    public SonarCloudApiClientException()
    {
    }

    public SonarCloudApiClientException(string? message) : base(message)
    {
    }

    public SonarCloudApiClientException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
