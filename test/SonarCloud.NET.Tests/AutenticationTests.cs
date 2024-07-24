using SonarCloud.NET.Client;
using SonarCloud.NET.Helpers;
using SonarCloud.NET.Tests.Helper;
using System.Net;

namespace SonarCloud.NET.Tests;

public class AutenticationTests
{
    public class Logout
    {
        [Fact]
        public async Task Should_Call_Logout()
        {
            var response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"")
            };

            var client = TestsHelper.GetClient(response);

            await client.Logout();
        }

        [Fact]
        public async Task Should_Throw_If_ErrorStatusCode()
        {
            var response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = new StringContent(@"")
            };

            var client = TestsHelper.GetClient(response);

            var request = () => client.Logout();

            await request.Should().ThrowAsync<SonarCloudApiClientException>();
        }
    }

    public class Validate
    {
        [Fact]
        public async Task Should_Return_true()
        {
            var response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\"valid\": true}")
            };

            var client = TestsHelper.GetClient(response);

            var result = await client.Validate();

            result.Should().BeTrue();
        }
    }
}
