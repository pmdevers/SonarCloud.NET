using SonarCloud.NET.Apis;
using SonarCloud.NET.Tests.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SonarCloud.NET.Tests;
public class UsersApiTests
{
    public class Groups
    {
        [Fact]
        public async Task Ok_Response()
        {
            var response = SnapshotReader<UsersApi>.Read(HttpStatusCode.OK, "groups");

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/users/groups?login=admin&organization=my-org");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);


            var result = await client.Users.Groups(new()
            {
                Login = "admin",
                Organization = "my-org"
            });

            result.Groups.Should().NotBeNull();
            result.Groups.Should().HaveCount(2);
        }
    }

    public class Search
    {
        [Fact]
        public async Task Ok_Response()
        {
            var response = SnapshotReader<UsersApi>.Read(HttpStatusCode.OK, "search");

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/users/search?ids=1%2c2");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);


            var result = await client.Users.Search(new()
            {
                Ids = "1,2"
            });

            result.Users.Should().NotBeNull();
        }
    }
}
