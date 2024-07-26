
namespace SonarCloud.NET.Tests.Helper;
public class RequestValidationHandler(Func<HttpRequestMessage, bool> validate, HttpResponseMessage response) : DelegatingHandler
{

    public bool IsValidRequest { get; set; }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        IsValidRequest = validate(request);
        return Task.FromResult(response);
    }
}
