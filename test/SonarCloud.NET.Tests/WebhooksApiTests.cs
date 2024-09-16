using SonarCloud.NET.Apis;
using SonarCloud.NET.Tests.Helper;
using System.Net;

namespace SonarCloud.NET.Tests;
public class WebhooksApiTests
{
    public class Create
    {
        [Fact]
        public async Task Ok_Response()
        {
            var response = SnapshotReader<WebhooksApi>.Read(HttpStatusCode.OK, "create");

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/webhooks/create");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);


            var result = await client.Webhooks.Create(new()
            {
                Name = Guid.NewGuid().ToString(),
                Organization = Guid.NewGuid().ToString(),
                Url = new Uri("http://www.acme.com")
            });

            result.Webhook.Should().NotBeNull();
        }
    }
    public class Delete()
    {
        [Fact]
        public async Task Ok_Reponse()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/webhooks/delete");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);


            await client.Webhooks.Delete(new() 
            { 
                Webhook = Guid.NewGuid().ToString()
            });
        }
    }

    public class Deliveries
    {
        [Fact]
        public async Task Ok_Response()
        {
            var response = SnapshotReader<WebhooksApi>.Read(HttpStatusCode.OK, "deliveries");

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/webhooks/deliveries");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);


            var result = await client.Webhooks.GetDeliveries(new()
            {
                
            });

            result.Paging.Index.Should().Be(1);
            result.Deliveries.Should().HaveCount(1);
        }
    }

    public class Delivery
    {
        [Fact]
        public async Task Ok_Response()
        {
            var response = SnapshotReader<WebhooksApi>.Read(HttpStatusCode.OK, "delivery");

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/webhooks/delivery?deliveryId=d1");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);


            var result = await client.Webhooks.GetDelivery(new()
            {
                DeliveryId = "d1"
            });

            result.Delivery.Should().NotBeNull();
            result.Delivery?.Id.Should().Be("d1");
        }
    }

    public class List
    {
        [Fact]
        public async Task Ok_Response()
        {
            var response = SnapshotReader<WebhooksApi>.Read(HttpStatusCode.OK, "list");

            var handler = new RequestValidationHandler((r) =>
            {
                r.RequestUri.Should().Be("https://sonarcloud.io/api/webhooks/list?organization=tjip");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);


            var result = await client.Webhooks.GetList(new()
            {
                Organization = "tjip"
            });

            result.Webhooks.Should().NotBeNull();
            result.Webhooks.Should().HaveCount(2);
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
                r.RequestUri.Should().Be("https://sonarcloud.io/api/webhooks/update");
                return true;
            }, response);

            var client = TestsHelper.GetClient(handler);


            await client.Webhooks.Update(new()
            {
                Name = "name",
                Url = new Uri("http://webhook.com/"),
                Webhook = "id1"
            });
        }
    }
}
