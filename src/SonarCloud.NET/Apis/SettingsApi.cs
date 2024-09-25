using SonarCloud.NET.Helpers;
using SonarCloud.NET.Models;
using System.Text.Json.Serialization;

namespace SonarCloud.NET.Apis;

public interface ISettingsApi
{
    /// <summary>
    /// List settings definitions.
    /// Requires 'Browse' permission when a component is specified <br />
    /// To access licensed settings, authentication is required <br />
    /// To access secured settings, one of the following permissions is required: <br />
    /// <list type="bullet">
    ///   <item>'Execute Analysis'</item>
    ///   <item>'Administer' rights on the specified component</item>
    /// </list>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<SettingsListDefinitionsResponse> ListDefinitions(SettingsListDefinitionsRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove a setting value. <br />
    /// The settings defined in conf/sonar.properties are read-only and can't be changed.<br />
    /// Requires the permission 'Administer' on the specified component.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task Reset(SettingsResetRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a setting value. <br />
    /// Either 'value' or 'values' must be provided. <br />
    /// The settings defined in conf/sonar.properties are read-only and can't be changed. <br />
    /// Requires the permission 'Administer' on the specified component. <br />
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task Set(SettingsSetRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// List settings values.
    /// If no value has been set for a setting, then the default value is returned.
    /// Both component and organization parameters cannot be used together.
    /// Requires 'Browse' or 'Execute Analysis' permission when a component is specified.
    /// Requires to be member of the organization if one is specified.
    /// To access secured settings, one of the following permissions is required: 'Execute Analysis' or 'Administer' rights on the specified component
    /// The returned attributes are:
    /// <list type="bullet">
    ///   <item>'key': The key of the setting </item>
    ///   <item>'value': The value of setting </item>
    ///   <item>'inherited': True if the value is being inherited from a parent setting</item>
    ///   <item>'parentValue: The value of the parent setting if the value is not inherited'</item>
    ///   <item>'parentOrigin: The origin of the parentValue (INSTANCE, ORGANIZATION, PROJECT)'</item>
    /// </list>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<SettingsValuesResponse> Values(SettingsValuesRequest request, CancellationToken cancellationToken = default);
}

public class SettingsValuesResponse
{
    public required Setting[] Settings { get; init; }
}

public record Setting(string Key, string Value, string[] values, bool Inherited, string ParentOrigin, FieldValue[] FieldValues);
public record FieldValue(string Boolean, string Text);

public class SettingsValuesRequest
{
    /// <summary>
    /// Component key
    /// </summary>
    [QueryString("component")]
    public string? Component { get; init; }

    /// <summary>
    /// Comma-separated list of keys
    /// </summary>
    [QueryString("keys")]
    public required string Keys { get; init; }

    /// <summary>
    /// Organization key
    /// </summary>
    [QueryString("organization")]
    public string? Organization { get; init; }
}

public class SettingsSetRequest
{
    /// <summary>
    /// Component key
    /// </summary>
    [QueryString("component")]
    public string? Component { get; init; }

    /// <summary>
    /// Setting field values. To set several values, the parameter must be called once for each value.
    /// </summary>
    [QueryString("fieldValues")]
    public string? FieldValues { get; init; }

    /// <summary>
    /// Setting Key
    /// </summary>
    [QueryString("key")]
    public required string Key { get; init; }

    /// <summary>
    /// Organization key (for the Enterprise plan only)
    /// </summary>
    [QueryString("organization")]
    public string? Organization { get; init; }

    /// <summary>
    /// Setting value. To reset a value, please use the reset web service.
    /// </summary>
    [QueryString("value")]
    public string? Value { get; init; }

    /// <summary>
    /// Setting value. To reset a value, please use the reset web service.
    /// </summary>
    [QueryString("values")]
    public string? Values { get; init; }
}

public class SettingsResetRequest
{
    /// <summary>
    /// Branch key
    /// </summary>
    [QueryString("branch")]
    public string? Branch { get; init; }

    /// <summary>
    /// Component key
    /// </summary>
    [QueryString("component")]
    public string? Component { get; init; }

    /// <summary>
    /// Comma-separated list of keys
    /// </summary>
    [QueryString("keys")]
    public required string Keys { get; init; }

    /// <summary>
    /// Organization key
    /// </summary>
    [QueryString("organization")]
    public string? Organization { get; init; }

    /// <summary>
    /// Pull request id
    /// </summary>
    [QueryString("pullRequest")]
    public string? PullRequest { get; init; }
}

public class SettingsListDefinitionsResponse
{
    [JsonPropertyName("definitions")]
    public List<Definition> Definitions { get; set; } = [];
}

public class SettingsListDefinitionsRequest
{
    /// <summary>
    /// Component Key
    /// </summary>
    [QueryString("component")]
    public string? Component { get; init; }
}

internal sealed class SettingsApi(SonarCloudApiClient client) : ISettingsApi
{
    private const string Endpoint = "api/settings";

    public Task<SettingsListDefinitionsResponse> ListDefinitions(SettingsListDefinitionsRequest request, CancellationToken cancellationToken = default)
        => client.Get<SettingsListDefinitionsRequest, SettingsListDefinitionsResponse>($"{Endpoint}/list_definitions", request, cancellationToken);

    public Task Reset(SettingsResetRequest request, CancellationToken cancellationToken = default)
        => client.Post($"{Endpoint}/reset", request,cancellationToken);

    public Task Set(SettingsSetRequest request, CancellationToken cancellationToken = default) 
        => client.Post($"{Endpoint}/set", request, cancellationToken);

    public Task<SettingsValuesResponse> Values(SettingsValuesRequest request, CancellationToken cancellationToken = default)
        => client.Get<SettingsValuesRequest, SettingsValuesResponse>($"{Endpoint}/values", request, cancellationToken);
}
