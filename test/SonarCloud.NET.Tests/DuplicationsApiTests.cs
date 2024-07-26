using SonarCloud.NET.Tests.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SonarCloud.NET.Tests;
public  class DuplicationsApiTests
{
    public class Show
    {
        [Fact]
        public async Task should_have_expected_request_and_response()
        {
            var response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"
{
  ""duplications"": [
    {
      ""blocks"": [
        {
          ""from"": 94,
          ""size"": 101,
          ""_ref"": ""1""
        },
        {
          ""from"": 83,
          ""size"": 101,
          ""_ref"": ""2""
        }
      ]
    },
    {
      ""blocks"": [
        {
          ""from"": 38,
          ""size"": 40,
          ""_ref"": ""1""
        },
        {
          ""from"": 29,
          ""size"": 39,
          ""_ref"": ""2""
        }
      ]
    },
    {
      ""blocks"": [
        {
          ""from"": 148,
          ""size"": 24,
          ""_ref"": ""1""
        },
        {
          ""from"": 137,
          ""size"": 24,
          ""_ref"": ""2""
        },
        {
          ""from"": 137,
          ""size"": 24,
          ""_ref"": ""3""
        }
      ]
    }
  ],
  ""files"": {
    ""1"": {
      ""key"": ""org.codehaus.sonar:sonar-plugin-api:src/main/java/org/sonar/api/utils/command/CommandExecutor.java"",
      ""name"": ""CommandExecutor"",
      ""projectName"": ""SonarQube""
    },
    ""2"": {
      ""key"": ""com.sonarsource.orchestrator:sonar-orchestrator:src/main/java/com/sonar/orchestrator/util/CommandExecutor.java"",
      ""name"": ""CommandExecutor"",
      ""projectName"": ""SonarSource :: Orchestrator""
    },
    ""3"": {
      ""key"": ""org.codehaus.sonar.runner:sonar-runner-api:src/main/java/org/sonar/runner/api/CommandExecutor.java"",
      ""name"": ""CommandExecutor"",
      ""projectName"": ""SonarSource Runner""
    }
  }
}
                ")
            };

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/duplications/show");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);


            var result = await client.Duplications.Show(new()
            {

            });

            result.Duplications.Should().HaveCount(3);
            result.Files.Should().HaveCount(3);
        }
    }
}
