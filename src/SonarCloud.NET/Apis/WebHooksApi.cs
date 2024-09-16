using SonarCloud.NET.Helpers;
using SonarCloud.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SonarCloud.NET.Apis;

/// <summary>
/// Webhooks allow to notify external services when a project analysis is done

/// </summary>
public interface IWebHooksApi
{
    /// <summary>
    /// Create a Webhook.
    /// Requires 'Administer' permission on the specified project.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<CreateWebhookResponse> Create(CreateWebhookRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a Webhook.
    /// Requires 'Administer' permission on the specified project, or global 'Administer' permission.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task Delete(DeleteWebhookRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get the recent deliveries for a specified project or Compute Engine task.
    /// Require 'Administer' permission on the related project.
    /// Note that additional information are returned by api/webhooks/delivery.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<GetWebhooksDeliveriesResponse> GetDeliveries(GetWebhooksDeliveriesRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a webhook delivery by its id.
    /// Note that additional information are returned by api/webhooks/delivery.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<GetWebhookDeliveryResponse> GetDelivery(GetWebhooksDeliveryRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Search for global webhooks or project webhooks. Webhooks are ordered by name.
    /// Requires 'Administer' permission on the specified project, or global 'Administer' permission.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<GetWebhooksListResponse> GetList(GetWebhooksListRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a Webhook.
    /// Requires 'Administer' permission on the specified project, or global 'Administer' permission.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    Task Update(UpdateWebhookRequest request, CancellationToken cancellationToken = default);
}


public class UpdateWebhookRequest
{
    /// <summary>
    /// new name of the webhook
    /// </summary>
    [QueryString("name")]
    public required string Name { get; set; }

    /// <summary>
    /// If provided, secret will be used as the key to generate the HMAC hex (lowercase) digest value in the 'X-Sonar-Webhook-HMAC-SHA256' header. If blank, any secret previously configured will be removed. If not set, the secret will remain unchanged.
    /// </summary>
    [QueryString("secret")]
    public string? Secret { get; set; }

    /// <summary>
    /// new url to be called by the webhook
    /// </summary>
    [QueryString("url")]
    public required Uri Url { get; set; }

    /// <summary>
    /// The key of the webhook to be updated, auto-generated value can be obtained through api/webhooks/create or api/webhooks/list
    /// </summary>
    [QueryString("webhook")]
    public required string Webhook { get; set; }
}

public class GetWebhooksListRequest
{
    /// <summary>
    /// Organization
    /// </summary>
    [QueryString("organization")]
    public required string Organization { get; set; }

    /// <summary>
    /// Project key
    /// </summary>
    [QueryString("project")]
    public string? Project { get; set; }
}

public class GetWebhooksListResponse
{
    [JsonPropertyName("webhooks")]
    public Webhook[] Webhooks { get; set; } = [];
}


public class GetWebhooksDeliveryRequest
{
    /// <summary>
    /// Id of delivery
    /// </summary>
    [QueryString("deliveryId")]
    public required string DeliveryId { get; set; }
}

public class GetWebhookDeliveryResponse
{
    public Delivery? Delivery { get; set; }
}

public class GetWebhooksDeliveriesRequest
{
    /// <summary>
    /// Id of the Compute Engine task
    /// </summary>
    [QueryString("ceTaskId")]
    public string? CeTaskId { get; set; }

    /// <summary>
    /// Key of the project
    /// </summary>
    [QueryString("componentKey")]
    public string? ComponentKey { get; set; }

    /// <summary>
    /// 1-based page number
    /// </summary>
    [QueryString("p")]
    public string? PageNumber { get; set; }

    /// <summary>
    /// Page size. Must be greater than 0 and less than 500
    /// </summary>
    [QueryString("ps")]
    public string? PageSize { get; set; }

    /// <summary>
    /// Key of the webhook that triggered those deliveries, auto-generated value that can be obtained through api/webhooks/create or api/webhooks/list
    /// </summary>
    [QueryString("webhook")]
    public string? Webhook { get; set; }
}

public class GetWebhooksDeliveriesResponse
{
    [JsonPropertyName("paging")]
    public Paging Paging { get; set; } = new();

    [JsonPropertyName("deliveries")]
    public List<Delivery> Deliveries { get; set; } = [];
}

public class DeleteWebhookRequest
{
    /// <summary>
    /// The key of the webhook to be deleted, auto-generated value can be obtained through api/webhooks/create or api/webhooks/list
    /// </summary>
    public required string Webhook { get; set; }
}

public class CreateWebhookRequest
{
    /// <summary>
    /// Name displayed in the administration console of webhooks
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The key of the organization that will own the webhook
    /// </summary>
    public required string Organization { get;set; }

    /// <summary>
    /// The key of the project that will own the webhook
    /// </summary>
    public string? Project { get; set; }

    /// <summary>
    /// If provided, secret will be used as the key to generate the HMAC hex (lowercase) digest value in the 'X-Sonar-Webhook-HMAC-SHA256' header
    /// </summary>
    public string? Secret { get; set; }

    /// <summary>
    /// Server endpoint that will receive the webhook payload, 
    /// for example 'http://my_server/foo'. If HTTP Basic authentication is used, 
    /// HTTPS is recommended to avoid man in the middle attacks. 
    /// Example: 'https://myLogin:myPassword@my_server/foo'
    /// </summary>
    public required Uri Url { get; set; }
}

public class CreateWebhookResponse
{
    public required Webhook Webhook { get; set; }
}


internal sealed class WebhooksApi(SonarCloudApiClient client) : IWebHooksApi
{
    private const string Endpoint = "api/webhooks";
    public Task<CreateWebhookResponse> Create(CreateWebhookRequest request, CancellationToken cancellationToken = default)
        => client.Post<CreateWebhookRequest, CreateWebhookResponse>($"{Endpoint}/create", request, cancellationToken);

    public Task Delete(DeleteWebhookRequest request, CancellationToken cancellationToken = default)
        => client.Post($"{Endpoint}/delete", request, cancellationToken);

    public Task<GetWebhooksDeliveriesResponse> GetDeliveries(GetWebhooksDeliveriesRequest request, CancellationToken cancellationToken = default)
        => client.Get<GetWebhooksDeliveriesRequest, GetWebhooksDeliveriesResponse>($"{Endpoint}/deliveries", request, cancellationToken);

    public Task<GetWebhookDeliveryResponse> GetDelivery(GetWebhooksDeliveryRequest request, CancellationToken cancellationToken = default)
        => client.Get<GetWebhooksDeliveryRequest, GetWebhookDeliveryResponse>($"{Endpoint}/delivery", request, cancellationToken);

    public Task<GetWebhooksListResponse> GetList(GetWebhooksListRequest request, CancellationToken cancellationToken = default)
        => client.Get<GetWebhooksListRequest, GetWebhooksListResponse>($"{Endpoint}/list", request, cancellationToken);

    public Task Update(UpdateWebhookRequest request, CancellationToken cancellationToken = default)
        => client.Post($"{Endpoint}/update", request, cancellationToken);
}
