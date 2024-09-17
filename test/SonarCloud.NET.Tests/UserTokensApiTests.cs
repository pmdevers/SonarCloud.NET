using SonarCloud.NET.Apis;
using SonarCloud.NET.Tests.Helper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SonarCloud.NET.Tests;
public class UserTokensApiTests
{
    public class Generate
    {
        [Fact]
        public async Task CallsWith_Parameter()
        {
            var response = SnapshotReader<UserTokensApi>.Read(HttpStatusCode.OK, "generate");

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/user_tokens/generate");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);

            var result = await client.UserTokens.Generate(new()
            {
                Login = "grace.hopper",
                Name = "Third Party Application",
            });

            var date = DateTime.Parse("2018-01-10T14:06:05+0100", CultureInfo.InvariantCulture);

            result.Should().NotBeNull();
            result.Login.Should().Be("grace.hopper");
            result.Name.Should().Be("Third Party Application");
            result.CreatedAt.Should().Be(date);
            result.Token.Should().Be("123456789");
        }
    }

    public class Revoke 
    {
        [Fact]
        public async Task CallsWith_Parameter()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/user_tokens/revoke");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);
            await client.UserTokens.Revoke(new()
            {
                Name = "Third Party Application"
            });
        }
    }

    public class Search
    {
        [Fact]
        public async Task CallsWith_Parameter()
        {
            var response = SnapshotReader<UserTokensApi>.Read(HttpStatusCode.OK, "search");

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/user_tokens/search?login=grace.hopper");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);

            var result = await client.UserTokens.Search(new()
            {
                Login = "grace.hopper",
            });

            result.Should().NotBeNull();
            result.Login.Should().Be("grace.hopper");
            result.UserTokens.Should().HaveCount(3);
        }
    }
}
