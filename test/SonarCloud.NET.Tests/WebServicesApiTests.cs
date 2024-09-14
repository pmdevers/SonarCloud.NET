using SonarCloud.NET.Tests.Helper;
using System.Net;

namespace SonarCloud.NET.Tests;
public class WebServicesApiTests
{
    [Fact]
    public async Task List_Should_Call()
    {
        var response = SnapshotReader<WebServicesApi>.Read(HttpStatusCode.OK, "list");

        var handler = new RequestValidationHandler((r) =>
        {
            r.RequestUri.Should().Be("https://sonarcloud.io/api/webservices/list");
            return true;
        }, response);

        var client = TestsHelper.GetClient(handler);


        var result = await client.WebServices.List();

        result.WebServices.Should().HaveCount(2);
    }

    [Fact]
    public async Task Example()
    {
        var response = SnapshotReader<WebServicesApi>.Read(HttpStatusCode.OK, "example");

        var handler = new RequestValidationHandler((r) =>
        {
            r.RequestUri.Should().Be("https://sonarcloud.io/api/webservices/response_example?action=action&controller=controller");
            return true;
        }, response);

        var client = TestsHelper.GetClient(handler);


        var result = await client.WebServices.ResponseExample(new()
        {
            Action = "action",
            Controller = "controller"
        });

        result.Format.Should().NotBeNullOrEmpty();
    }
}
