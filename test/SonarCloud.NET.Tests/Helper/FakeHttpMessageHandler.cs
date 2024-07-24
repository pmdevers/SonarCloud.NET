using System.Net;
using System.Text;

namespace SonarCloud.NET.Tests.Helper;
public class FakeHttpMessageHandler(HttpResponseMessage responseMessage) : DelegatingHandler
{
    private readonly HttpResponseMessage _fakeResponse = responseMessage;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_fakeResponse);
    }
}
