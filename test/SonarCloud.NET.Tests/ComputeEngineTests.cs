using SonarCloud.NET.Tests.Helper;
using System.Net;

namespace SonarCloud.NET.Tests;
public class ComputeEngineTests
{
    public class Search
    {
        [Fact]
        public async Task should_have_expected_request_and_response()
        {
            var response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"
    {
  ""tasks"": [
    {
      ""organization"": ""my-org-1"",
      ""id"": ""BU_dO1vsORa8_beWCwsP"",
      ""type"": ""REPORT"",
      ""componentId"": ""AU-Tpxb--iU5OvuD2FLy"",
      ""componentKey"": ""project_1"",
      ""componentName"": ""Project One"",
      ""componentQualifier"": ""TRK"",
      ""analysisId"": ""AU-TpxcB-iU5Ovu12345"",
      ""status"": ""SUCCESS"",
      ""submittedAt"": ""2015-08-13T23:34:59+0200"",
      ""submitterLogin"": ""john"",
      ""startedAt"": ""2015-08-13T23:35:00+0200"",
      ""executedAt"": ""2015-08-13T23:35:10+0200"",
      ""executionTimeMs"": 10000,
      ""logs"": false,
      ""hasErrorStacktrace"": false,
      ""hasScannerContext"": true
    },
    {
      ""organization"": ""my-org-2"",
      ""id"": ""AU_dO1vsORa8_beWCwmP"",
      ""taskType"": ""REPORT"",
      ""componentId"": ""AU_dO1vlORa8_beWCwmO"",
      ""componentKey"": ""project_2"",
      ""componentName"": ""Project Two"",
      ""componentQualifier"": ""TRK"",
      ""status"": ""FAILED"",
      ""submittedAt"": ""2015-09-17T23:34:59+0200"",
      ""startedAt"": ""2015-09-17T23:35:00+0200"",
      ""executedAt"": ""2015-08-13T23:37:00+0200"",
      ""executionTimeMs"": 120000,
      ""logs"": false,
      ""errorMessage"": ""Failed to unzip analysis report"",
      ""hasErrorStacktrace"": true,
      ""hasScannerContext"": true
    }
  ]
}
                ")
            };

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/ce/activity");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);
                       

            var result = await client.ComputeEngine.Search(new()
            {

            });

            result.Tasks.Should().HaveCount(2);
        }
    }

    public class GetActivityStatus
    {
        [Fact]
        public async Task should_have_expected_request_and_response()
        {
            var response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"
{
  ""pending"": 2,
  ""inProgress"": 1,
  ""failing"": 5,
  ""pendingTime"": 100123
}
                ")
            };

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/ce/activity_status");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);


            var result = await client.ComputeEngine.GetActivityStatus(new()
            {
                
            });

            result.Pending.Should().Be(2);
            result.InProgress.Should().Be(1);
            result.Failing.Should().Be(5);
            result.PendingTime.Should().Be(100123);
        }
    }

    public class GetPendingTasks
    {
        [Fact]
        public async Task should_have_expected_request_and_response()
        {
            var response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"
{
  ""queue"": [
    {
      ""organization"": ""my-org-1"",
      ""id"": ""AU_w84A6gAS1Hm6h4_ih"",
      ""type"": ""REPORT"",
      ""componentId"": ""AU_w74XMgAS1Hm6h4-Y-"",
      ""componentKey"": ""com.github.kevinsawicki:http-request-parent"",
      ""componentName"": ""HttpRequest"",
      ""componentQualifier"": ""TRK"",
      ""status"": ""PENDING"",
      ""submittedAt"": ""2015-09-21T19:28:54+0200"",
      ""logs"": false
    }
  ],
  ""current"": {
    ""organization"": ""my-org-1"",
    ""id"": ""AU_w8LDjgAS1Hm6h4-aY"",
    ""type"": ""REPORT"",
    ""componentId"": ""AU_w74XMgAS1Hm6h4-Y-"",
    ""componentKey"": ""com.github.kevinsawicki:http-request-parent"",
    ""componentName"": ""HttpRequest"",
    ""componentQualifier"": ""TRK"",
    ""analysisId"": ""123456"",
    ""status"": ""FAILED"",
    ""submittedAt"": ""2015-09-21T19:25:49+0200"",
    ""startedAt"": ""2015-09-21T19:25:57+0200"",
    ""finishedAt"": ""2015-09-21T19:25:58+0200"",
    ""executionTimeMs"": 1371,
    ""logs"": false,
    ""errorMessage"": ""the error message"",
    ""errorType"": ""the optional error type"",
    ""hasErrorStacktrace"": false,
    ""hasScannerContext"": true
  }
}
                ")
            };

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/ce/component");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);


            var result = await client.ComputeEngine.GetPendingTasks(new()
            {

            });

            result.Queue.Should().HaveCount(1);
            result.Current.Should().NotBeNull();
            result.Current?.Status.Should().Be("FAILED");
        }
    }

    public class GetTaskDetails
    {
        public class GetPendingTasks
        {
            [Fact]
            public async Task should_have_expected_request_and_response()
            {
                var guid = Guid.NewGuid().ToString();
                var response = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(@"
{
  ""task"": {
    ""organization"": ""my-org-1"",
    ""id"": ""AVAn5RKqYwETbXvgas-I"",
    ""type"": ""REPORT"",
    ""componentId"": ""AVAn5RJmYwETbXvgas-H"",
    ""componentKey"": ""project_1"",
    ""componentName"": ""Project One"",
    ""componentQualifier"": ""TRK"",
    ""analysisId"": ""123456"",
    ""status"": ""FAILED"",
    ""submittedAt"": ""2015-10-02T11:32:15+0200"",
    ""startedAt"": ""2015-10-02T11:32:16+0200"",
    ""executedAt"": ""2015-10-02T11:32:22+0200"",
    ""executionTimeMs"": 5286,
    ""errorMessage"": ""Fail to extract report AVaXuGAi_te3Ldc_YItm from database"",
    ""logs"": false,
    ""hasErrorStacktrace"": true,
    ""errorStacktrace"": ""java.lang.IllegalStateException: Fail to extract report AVaXuGAi_te3Ldc_YItm from database\n\tat org.sonar.server.computation.task.projectanalysis.step.ExtractReportStep.execute(ExtractReportStep.java:50)"",
    ""scannerContext"": ""SonarQube plugins:\n\t- Git 1.0 (scmgit)\n\t- Java 3.13.1 (java)"",
    ""hasScannerContext"": true
  }
}
                ")
                };

                var handler = new RequestValidationHandler((r) =>
                {
                    r.RequestUri.Should().Be($"https://sonarcloud.io/api/ce/task?id={guid}");
                    return true;
                }, response);

                var client = TestsHelper.GetClient(handler);


                var result = await client.ComputeEngine.GetTaskDetails(new()
                {
                    Id = guid
                });
                                
                result.Task.Should().NotBeNull();
                result.Task?.Status.Should().Be("FAILED");
            }
        }
    }
}
