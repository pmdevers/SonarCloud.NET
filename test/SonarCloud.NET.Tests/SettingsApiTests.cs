using SonarCloud.NET.Apis;
using SonarCloud.NET.Tests.Helper;
using System.Net;

namespace SonarCloud.NET.Tests;
public class SettingsApiTests
{
    public class ListDefinitions
    {
        [Fact]
        public async Task Ok_Response()
        {
            var response = SnapshotReader<SettingsApi>.Read(HttpStatusCode.OK, "list_definitions");

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/settings/list_definitions?component=test");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);


            var result = await client.Settings.ListDefinitions(new()
            { 
                Component = "test"
            });

            result.Should().NotBeNull();
        }
    }

    public class Reset
    {
        [Fact]
        public async Task Ok_Response()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/settings/reset");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);


            await client.Settings.Reset(new()
            {
                Keys = "test,test1"
            });
        }
    }

    public class Set
    {
        [Fact]
        public async Task Ok_Response()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/settings/set");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);


            await client.Settings.Set(new()
            {
                Key = "test"
            });
        }
    }

    public class Values
    {
        [Fact]
        public async Task Ok_Response()
        {
            var response = SnapshotReader<SettingsApi>.Read(HttpStatusCode.OK, "values");

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/settings/values?keys=sonar.test.jira");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);


            var result = await client.Settings.Values(new()
            {
                 Keys = "sonar.test.jira"
            });

            result.Should().NotBeNull();
        }
    }
}
