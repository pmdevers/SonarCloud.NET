using SonarCloud.NET.Apis;
using SonarCloud.NET.Tests.Helper;
using System.Net;

namespace SonarCloud.NET.Tests;
public class UserGroupsApiTests
{
    public class AddUser
    {
        [Fact]
        public async Task Ok_Response()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/user_groups/add_user");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);


            await client.UserGroups.AddUser(new()
            {
                Login = "admin",
                Organization = "my-org"
            });
        }
    }

    public class Create
    {
        [Fact]
        public async Task Ok_Response()
        {
            var response = SnapshotReader<UserGroupsApi>.Read(HttpStatusCode.OK, "create");

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/user_groups/create");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);


            var result = await client.UserGroups.Create(new()
            {
                Name = "sonar-users",
                Organinzation = "my-org"
            });

            result.Group.Should().NotBeNull();
        }
    }

    public class Delete
    {
        [Fact]
        public async Task Ok_Response()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/user_groups/delete");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);


            await client.UserGroups.Delete(new()
            {
                Organization = "my-org"
            });
        }
    }

    public class RemoveUser
    {
        [Fact]
        public async Task Ok_Response()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/user_groups/remove_user");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);


            await client.UserGroups.RemoveUser(new()
            {
                Id = 42
            });
        }
    }

    public class Search
    {
        [Fact]
        public async Task Ok_Response()
        {
            var response = SnapshotReader<UserGroupsApi>.Read(HttpStatusCode.OK, "search");

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/user_groups/search?organization=my-org");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);


            var result = await client.UserGroups.Search(new()
            {
                Organization = "my-org"
            });

            result.Groups.Should().NotBeNull();
            result.Groups.Should().HaveCount(2);
        }
    }

    public class Update
    {
        [Fact]
        public async Task Ok_Response()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/user_groups/update");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);

            await client.UserGroups.Update(new()
            {
                Id = 42,
                Name = "sonar-admins" 
            });
        }
    }

    public class Users
    {
        [Fact]
        public async Task Ok_Response()
        {
            var response = SnapshotReader<UserGroupsApi>.Read(HttpStatusCode.OK, "users");

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/user_groups/users?organization=my-org");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);


            var result = await client.UserGroups.Users(new()
            {
                Organization = "my-org"
            });

            result.Users.Should().NotBeNull();
            result.Users.Should().HaveCount(2);
        }
    }
}
