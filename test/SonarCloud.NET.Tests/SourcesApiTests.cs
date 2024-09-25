using SonarCloud.NET.Apis;
using SonarCloud.NET.Tests.Helper;
using System.Net;

namespace SonarCloud.NET.Tests;
public class SourcesApiTests
{
    public class Raw
    {
        [Fact]
        public async Task Ok_Response()
        {
            var response = SnapshotReader<SourcesApi>.Read(HttpStatusCode.OK, "raw");

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/sources/raw?key=my_project%3asrc%2ffoo%2fBar.php");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);


            var result = await client.Sources.Raw(new()
            {
                Key = "my_project:src/foo/Bar.php",
            });

            result.Should().NotBeNull();
        }
    }

    public class Scm
    {
        [Fact]
        public async Task Ok_Response()
        {
            var response = SnapshotReader<SourcesApi>.Read(HttpStatusCode.OK, "scm");

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/sources/scm?key=my_project%3asrc%2ffoo%2fBar.php");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);


            var result = await client.Sources.Scm(new()
            {
                Key = "my_project:src/foo/Bar.php",
            });

            result.Should().NotBeNull();
        }
    }


    public class Show
    {
        [Fact]
        public async Task Ok_Response()
        {
            var response = SnapshotReader<SourcesApi>.Read(HttpStatusCode.OK, "show");

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/sources/show?key=my_project%3asrc%2ffoo%2fBar.php");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);


            var result = await client.Sources.Show(new()
            {
                Key = "my_project:src/foo/Bar.php",
            });

            result.Should().NotBeNull();
        }
    }
}
