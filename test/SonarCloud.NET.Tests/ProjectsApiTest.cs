using SonarCloud.NET.Tests.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SonarCloud.NET.Tests;
public class ProjectsApiTest
{
    public class BlukDelete
    {
        [Fact]
        public async Task CallsWith_Parameter()
        {
            var response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\"valid\": true}")
            };

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/projects/bulk_delete");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);

            var request = new BulkDeleteRequest()
            {
                Organization = "test-key"
            };
            
            await client.Projects.BulkDelete(request);
        }
    }

    public class CreateProject
    {
        [Fact]
        public async Task CallsWith_Parameter()
        {
            var response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"
                {
                  ""project"": {
                    ""key"": ""project-key"",
                    ""name"": ""project-name"",
                    ""qualifier"": ""TRK""
                  }
                }")
            };

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/projects/create");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);

            var request = new CreateProjectsRequest()
            {
                Organization = "test-key",
                Name = "Test",
                Project = "test",
            };

            var result = await client.Projects.Create(request);

            result.Project.Name.Should().Be("project-name");
        }
    }

    public class UpdateVisibility { }
    public class UpdateKey
    {

    }
    public class SearchProjects 
    {
        [Fact]
        public async Task HandlesResults()
        {
            var response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""paging"":{""pageIndex"":1,""pageSize"":10,""total"":135},""components"":[{""organization"":""tjip"",""key"":""tjip_AbnAmro_aab-infrastructure-templates"",""name"":""AAB Infrastructure template test"",""qualifier"":""TRK"",""visibility"":""private"",""lastAnalysisDate"":""2023-11-07T13:22:41+0100"",""revision"":""db84a7088eb5d14d43b43c16bc52864a1639e96a""},{""organization"":""tjip"",""key"":""tjip_AbnAmro_abn-sp-datastore"",""name"":""AAB - SmartProjects datastore"",""qualifier"":""TRK"",""visibility"":""private"",""lastAnalysisDate"":""2023-04-05T09:11:57+0200"",""revision"":""7007f905c783dd76d459fbeb3a6736c8df3d7ec7""},{""organization"":""tjip"",""key"":""tjip_ABN360"",""name"":""ABN360"",""qualifier"":""TRK"",""visibility"":""private"",""lastAnalysisDate"":""2024-07-18T10:07:01+0200"",""revision"":""5de1aef751f94723d7afac4ca3f4f98a6f0ace60""},{""organization"":""tjip"",""key"":""tjip_AbnAmro_Hypotheek_AahHdnAdapterService"",""name"":""AbnAmro_Hypotheek_AahHdnAdapterService"",""qualifier"":""TRK"",""visibility"":""private"",""lastAnalysisDate"":""2024-07-11T15:05:10+0200"",""revision"":""8fcc01babadb6ff468cd7f9fcd5748a0efadebe4""},{""organization"":""tjip"",""key"":""tjip_AbnAmro_Hypotheek_haas-admin-ui"",""name"":""AbnAmro / Hypotheek - haas-admin-ui"",""qualifier"":""TRK"",""visibility"":""private"",""lastAnalysisDate"":""2024-06-04T16:16:30+0200"",""revision"":""af8e0f99518a6bae65ca045c1dec379c19432600""},{""organization"":""tjip"",""key"":""tjip_AbnAmro_Hypotheek_ket-ui"",""name"":""ABN AMRO KET UI"",""qualifier"":""TRK"",""visibility"":""private"",""lastAnalysisDate"":""2024-04-29T14:42:04+0200"",""revision"":""ec21d751821239ac7f6605e29eaf082a9ca16061""},{""organization"":""tjip"",""key"":""tjip_AbnAmro_abn-klantgegevens"",""name"":""ABN Klantgegevens API"",""qualifier"":""TRK"",""visibility"":""private"",""lastAnalysisDate"":""2023-10-18T13:33:12+0200"",""revision"":""61cd3b24e3d1dc273cad6f9cb0de35ffc4c18087""},{""organization"":""tjip"",""key"":""tjip_AbnAmro_abn-medewerkers-api"",""name"":""ABN Medewerkers API"",""qualifier"":""TRK"",""visibility"":""private"",""lastAnalysisDate"":""2024-04-03T09:21:34+0200"",""revision"":""73ce948c8d619b1abfc9e4d1467eb1d212d03c1b""},{""organization"":""tjip"",""key"":""tjip_AbnAmro_abn-medewerkers-ui"",""name"":""ABN Medewerkers UI"",""qualifier"":""TRK"",""visibility"":""private"",""lastAnalysisDate"":""2023-07-11T14:57:10+0200"",""revision"":""71f7e03a1da1e90e8d3214ca33f7200505fa5dc9""},{""organization"":""tjip"",""key"":""tjip_AbnAmro_abn-stukkenlijst-docker"",""name"":""ABN Stukkenlijst"",""qualifier"":""TRK"",""visibility"":""private"",""lastAnalysisDate"":""2022-04-08T13:34:08+0200"",""revision"":""8f5b67311c165eb143cab33cbb44520be4631fc5""}]}")
            };

            var client = TestsHelper.GetClient(response);

            var request = new SearchProjectsRequest()
            {
                Organization = "test-key",
            };

            var result = await client.Projects.Search(request);

            result.Should().NotBeNull();
        }    
        
    }
    public class DeleteProjects { }
    public class SearchProejects { }
}
