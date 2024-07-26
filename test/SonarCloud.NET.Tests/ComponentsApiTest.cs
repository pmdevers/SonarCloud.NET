using SonarCloud.NET.Tests.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SonarCloud.NET.Tests;
public class ComponentsApiTest
{
    public class Search
    {
        [Fact]
        public async Task Should_have_expected_request_and_response()
        {
            var response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"
{
  ""paging"": {
    ""pageIndex"": 1,
    ""pageSize"": 100,
    ""total"": 1
  },
  ""components"": [
    {
      ""organization"": ""my-org-1"",
      ""key"": ""project-key"",
      ""qualifier"": ""TRK"",
      ""name"": ""Project Name"",
      ""project"": ""project-key""
    }
  ]
}
                ")
            };

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/components/search?organization=test");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);


            var result = await client.Components.Search(new()
            {
                Organization = "test"
            });

            result.Components.Should().HaveCount(1);
        }
    }

    public class Show
    {
        [Fact]
        public async Task Should_have_expected_request_and_response()
        {
            var response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"
{
  ""component"": {
    ""organization"": ""my-org-1"",
    ""key"": ""com.sonarsource:java-markdown:src/main/java/com/sonarsource/markdown/impl/Rule.java"",
    ""name"": ""Rule.java"",
    ""qualifier"": ""FIL"",
    ""language"": ""java"",
    ""path"": ""src/main/java/com/sonarsource/markdown/impl/Rule.java"",
    ""analysisDate"": ""2017-03-01T11:39:03+0100"",
    ""leakPeriodDate"": ""2017-01-01T11:39:03+0100"",
    ""version"": ""1.1""
  },
  ""ancestors"": [
    {
      ""organization"": ""my-org-1"",
      ""key"": ""com.sonarsource:java-markdown:src/main/java/com/sonarsource/markdown/impl"",
      ""name"": ""src/main/java/com/sonarsource/markdown/impl"",
      ""qualifier"": ""DIR"",
      ""path"": ""src/main/java/com/sonarsource/markdown/impl"",
      ""analysisDate"": ""2017-03-01T11:39:03+0100"",
      ""version"": ""1.1""
    },
    {
      ""organization"": ""my-org-1"",
      ""key"": ""com.sonarsource:java-markdown"",
      ""name"": ""Java Markdown"",
      ""description"": ""Java Markdown Project"",
      ""qualifier"": ""TRK"",
      ""analysisDate"": ""2017-03-01T11:39:03+0100"",
      ""version"": ""1.1"",
      ""tags"": [
        ""language"",
        ""plugin""
      ],
      ""visibility"": ""private""
    }
  ]
}
                ")
            };

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/components/show?component=test");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);


            var result = await client.Components.Show(new()
            {
                Component = "test"
            });

            result.Component.Should().NotBeNull();
            result.Component?.Organization.Should().Be("my-org-1");
        }
    }

    public class Tree
    {
        [Fact]
        public async Task Should_have_expected_request_and_response()
        {
            var response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"
{
  ""paging"": {
    ""pageIndex"": 1,
    ""pageSize"": 100,
    ""total"": 3
  },
  ""baseComponent"": {
    ""organization"": ""my-org-1"",
    ""key"": ""MY_PROJECT_KEY"",
    ""description"": ""DESCRIPTION_MY_PROJECT_ID"",
    ""qualifier"": ""TRK"",
    ""tags"": [],
    ""visibility"": ""private""
  },
  ""components"": [
    {
      ""organization"": ""my-org-1"",
      ""key"": ""com.sonarsource:java-markdown:src/test/java/com/sonarsource/markdown/BasicMarkdownParser.java"",
      ""name"": ""BasicMarkdownParser.java"",
      ""qualifier"": ""UTS"",
      ""path"": ""src/test/java/com/sonarsource/markdown/BasicMarkdownParser.java"",
      ""language"":""java""
    },
    {
      ""organization"": ""my-org-1"",
      ""key"": ""com.sonarsource:java-markdown:src/test/java/com/sonarsource/markdown/BasicMarkdownParserTest.java"",
      ""name"": ""BasicMarkdownParserTest.java"",
      ""qualifier"": ""UTS"",
      ""path"": ""src/test/java/com/sonarsource/markdown/BasicMarkdownParserTest.java"",
      ""language"":""java""
    },
    {
      ""organization"": ""my-org-1"",
      ""key"": ""com.sonarsource:java-markdown:src/main/java/com/sonarsource/markdown"",
      ""name"": ""src/main/java/com/sonarsource/markdown"",
      ""qualifier"": ""DIR"",
      ""path"": ""src/main/java/com/sonarsource/markdown""
    }
  ]
}
                ")
            };

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/components/tree?component=test");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);


            var result = await client.Components.Tree(new()
            {
                Component= "test"
            });

            result.BaseComponent.Should().NotBeNull();
            result.BaseComponent?.Organization.Should().Be("my-org-1");
            result.Components.Should().HaveCount(3);
        }
    }
}
