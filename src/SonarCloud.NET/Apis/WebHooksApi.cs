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
    /// <returns></returns>
    Task<UpdateWebhookResponse> Update(UpdateWebhookRequest request, CancellationToken cancellationToken = default);
}

public class UpdateWebhookRequest
{
}

public class UpdateWebhookResponse
{
    
}

public class GetWebhooksListRequest
{
    [QueryString("organization")]
    public required string Organization { get; set; }
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
    [QueryString("deliveryId")]
    public required string DeliveryId { get; set; }
}

public class GetWebhookDeliveryResponse
{
    public Delivery? Delivery { get; set; }
}

public class GetWebhooksDeliveriesRequest
{
    [QueryString("ceTaskId")]
    public string? CeTaskId { get; set; }
    
    [QueryString("componentKey")]
    public string? ComponentKey { get; set; }
    
    [QueryString("p")]
    public string? PageNumber { get; set; }
    
    [QueryString("ps")]
    public string? PageSize { get; set; }

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
