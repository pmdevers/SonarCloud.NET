using SonarCloud.NET.Apis;
using SonarCloud.NET.Tests.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SonarCloud.NET.Tests;
public class RulesApiTests
{
    public class Repositories
    {
        [Fact]
        public async Task Ok_Response()
        {
            var response = SnapshotReader<RulesApi>.Read(HttpStatusCode.OK, "repositories");

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/rules/repositories?language=java&q=squid");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);


            var result = await client.Rules.Repositories(new()
            {
                Language = "java",
                Query = "squid"
            });

            result.Repositories.Should().NotBeNull();
        }
    }

    public class Search
    {
        [Fact]
        public async Task Ok_Response()
        {
            var response = SnapshotReader<RulesApi>.Read(HttpStatusCode.OK, "search");

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/rules/search?p=1&ps=25");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);


            var result = await client.Rules.Search(new()
            {
                PageNumber = 1,
                PageSize = 25
            });

            result.Rules.Should().NotBeNull();
        }
    }

    public class Show
    {
        [Fact]
        public async Task Ok_Response()
        {
            var response = SnapshotReader<RulesApi>.Read(HttpStatusCode.OK, "show");

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/rules/show?key=javascript%3aEmptyBlock&organization=my-org");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);


            var result = await client.Rules.Show(new()
            {
                Key = "javascript:EmptyBlock",
                Organization = "my-org"
            });

            result.Rule.Should().NotBeNull();
        }
    }

    public class Tags
    {
        [Fact]
        public async Task Ok_Response()
        {
            var response = SnapshotReader<RulesApi>.Read(HttpStatusCode.OK, "tags");

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/rules/tags?organization=my-org&ps=1&q=misra");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);


            var result = await client.Rules.Tags(new()
            {
                Organization = "my-org",
                PageSize = 1,
                Query = "misra"
            });

            result.Tags.Should().NotBeNull();
        }
    }

    public class Update
    {
        [Fact]
        public async Task Ok_Response()
        {
            var response = SnapshotReader<RulesApi>.Read(HttpStatusCode.OK, "update");

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/rules/update");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);


            var result = await client.Rules.Update(new()
            {
                Key = "javascript:NullCheck",
                Organization = "my-org",
            });

            result.Rule.Should().NotBeNull();
        }
    }
}
