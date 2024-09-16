using SonarCloud.NET.Helpers;
using SonarCloud.NET.Models;
using System.Text.Json.Serialization;

namespace SonarCloud.NET.Apis;


public interface IIssuesApi
{
    Task<AddCommentResponse> AddComment(AddCommentRequest request, CancellationToken cancellationToken = default);
    Task<AssignIssueResponse> AssignIssue(AssignIssueRequest request, CancellationToken cancellationToken = default);
    Task<SearchForAuthorsResponse> SearchForAuthors(SearchForAuthorsRequest request, CancellationToken cancellationToken = default);
    Task<BulkChangeResponse> BulkChange(BulkChangeRequest request, CancellationToken cancellationToken = default);
    Task<GetChangelogResponse> GetChangelog(GetChangelogRequest request, CancellationToken cancellationToken = default);
    Task<DeleteCommentResponse> DeleteComment(DeleteCommentRequest request, CancellationToken cancellationToken = default);

    Task<DoTransitionResponse> DoTransition(DoTransitionRequest request, CancellationToken cancellationToken = default);
    Task<EditCommentResponse> EditComment(EditCommentRequest request, CancellationToken cancellationToken = default);
    Task<SearchForIssuesResponse> SearchForIssues(SearchForIssuesRequest request, CancellationToken cancellationToken = default);

    Task<SetTagsResponse> SetTags(SetTagsRequest request, CancellationToken cancellationToken = default);
    Task<GetTagsResponse> GetTags(GetTagsRequest request, CancellationToken cancellationToken = default);

}

public class BulkChangeRequest
{
    [QueryString("add_tags")]
    public string? Tags { get; set; }

    [QueryString("assign")]
    public string? Assign { get; set; }

    [QueryString("do_transition")]
    public string? DoTransition { get; set; }

    [QueryString("isFeedback")]
    public bool? isFeedback { get; set; }

    [QueryString("issues")]
    public required string Issues { get; set; }

    [QueryString("remove_tags")]
    public string? RemoveTags { get; set; }

    [QueryString("sendNotifications")]
    public bool? SendNotifications { get; set; }

}

public class GetTagsRequest
{
    [QueryString("organization")]
    public string? OrganizationKey { get; set; }

    [QueryString("project")]
    public string? ProjectKey { get; set; }

    [QueryString("ps")]
    public int? PageSize { get; set; }

    [QueryString("q")]
    public string? Query { get; set; }
}

public class GetTagsResponse
{
    [JsonPropertyName("tags")]
    public string[] Tags { get; set; } = [];
}

public class SetTagsRequest
{
    [QueryString("issue")]
    public required string IssueKey { get; set; }

    [QueryString("tags")]
    public string? Tags { get; set; }
}

public class SetTagsResponse
{
    [JsonPropertyName("issue")]
    public required Issue Issue { get; set; }

    [JsonPropertyName("components")]
    public Component[] Components { get; set; } = [];

    [JsonPropertyName("rules")]
    public Rule[] Rules { get; set; } = [];

    [JsonPropertyName("users")]
    public User[] Users { get; set; } = [];
}

public class SearchForIssuesRequest
{
    [QueryString("additionalFields")]
    public string? AdditionalFields { get; set; }

    [QueryString("asc")]
    public bool? Ascending { get; set; }

    [QueryString("assigned")]
    public bool? Assigned { get; set; }

    [QueryString("assignees")]
    public string? Assignees { get; set; }

    [QueryString("author")]
    public string? Author { get; set; }

    [QueryString("branch")]
    public string? Branch { get; set; }

    [QueryString("cleanCodeAttributeCategories")]
    public string? CleanCodeAttributeCategories { get; set; }

    [QueryString("componentKeys")]
    public string? ComponentKeys { get; set; }

    [QueryString("createdAfter")]
    public DateTime? CreatedAfter { get; set; }

    [QueryString("createdAt")]
    public DateTime? CreatedAt { get; set; }

    [QueryString("createdBefore")]
    public DateTime? CreatedBefore { get; set; }

    [QueryString("createdInLast")]
    public string? CreatedInLast { get; set; }

    [QueryString("cwe")]
    public string? Cwe { get; set; }

    [QueryString("facets")]
    public string? Facets { get; set; }

    [QueryString("impactSeverities")]
    public string? ImpactSeverities { get; set; }

    [QueryString("impactSoftwareQualities")]
    public string? ImpactSoftwareQualities { get; set; }

    [QueryString("issueStatuses")]
    public string? IssueStatuses { get; set; }

    [QueryString("issues")]
    public string? Issues { get; set; }

    [QueryString("languages")]
    public string? Languages { get; set; }

    [QueryString("onComponentOnly")]
    public bool? OnComponentOnly { get; set; }

    [QueryString("organization")]
    public string? Organization { get; set; }

    [QueryString("owasTop10")]
    public string? OwasTop10 { get; set; }

    [QueryString("p")]
    public int? Page { get; set; }

    [QueryString("ps")]
    public int? PageSize { get; set; }

    [QueryString("pullRequest")]
    public int? PullRequestId { get; set; }

    [QueryString("resolved")]
    public bool? Resolved { get; set; }

    [QueryString("rules")]
    public string? Rules { get; set; }

    [QueryString("s")]
    public string? SortField { get; set; }

    [QueryString("sansTop25")]
    public string? SansTop25 { get; set; }

    [QueryString("sinceLeakPeriod")]
    public bool? sinceLeakPeriod { get; set; }

    [QueryString("sonarsourceSecurity")]
    public string? SonarsourceSecurity { get; set; }

    [QueryString("tags")]
    public string? Tags { get; set; }
}

public class SearchForIssuesResponse
{
    [JsonPropertyName("paging")]
    public required Paging Paging { get; set; }

    [JsonPropertyName("issues")]
    public Issue[] Issues { get; set; } = [];

    [JsonPropertyName("components")]
    public Component[] Components { get; set; } = [];

    [JsonPropertyName("rules")]
    public Rule[] Rules { get; set; } = [];

    [JsonPropertyName("users")]
    public User[] Users { get; set; } = [];
}

public class DoTransitionRequest
{
    [QueryString("comment")]
    public string? Comment { get; set; }

    [QueryString("isFeedback")]
    public bool? isFeedback { get; set; }

    [QueryString("issue")]
    public required string IssueKey { get; set; }

    [QueryString("transition")]
    public required string Transition { get; set; }
}

public class DoTransitionResponse
{
    [JsonPropertyName("issue")]
    public required Issue Issue { get; set; }

    [JsonPropertyName("components")]
    public Component[] Components { get; set; } = [];

    [JsonPropertyName("rules")]
    public Rule[] Rules { get; set; } = [];

    [JsonPropertyName("users")]
    public User[] Users { get; set; } = [];
}

public class EditCommentRequest
{
    [QueryString("comment")]
    public required string CommentKey { get; set; }

    [QueryString("text")]
    public required string Text { get; set; }
}

public class EditCommentResponse
{
    [JsonPropertyName("issue")]
    public required Issue Issue { get; set; }

    [JsonPropertyName("components")]
    public Component[] Components { get; set; } = [];

    [JsonPropertyName("rules")]
    public Rule[] Rules { get; set; } = [];

    [JsonPropertyName("users")]
    public User[] Users { get; set; } = [];
}

public class DeleteCommentRequest
{
    [QueryString("comment")]
    public required string CommentKey { get; set; }
}

public class DeleteCommentResponse
{
    [JsonPropertyName("issue")]
    public required Issue Issue { get; set; }

    [JsonPropertyName("components")]
    public Component[] Components { get; set; } = [];

    [JsonPropertyName("rules")]
    public Rule[] Rules { get; set; } = [];

    [JsonPropertyName("users")]
    public User[] Users { get; set; } = [];
}

public class GetChangelogRequest
{
    [JsonPropertyName("issue")]
    public required string IssueKey { get; set; }
}

public class GetChangelogResponse
{
    [JsonPropertyName("changelog")]
    public required Changelog Changelog { get; set; }
}

public class BulkChangeResponse
{
    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("success")]
    public int Success { get; set; }

    [JsonPropertyName("ignored")]
    public int Ignored { get; set; }

    [JsonPropertyName("failures")]
    public int Failures { get; set; }
}

public class SearchForAuthorsRequest
{
    [QueryString("organization")]
    public required string Organization { get; set; }

    [QueryString("project")]
    public string? Project { get; set; }

    [QueryString("ps")]
    public int? PageSize { get; set; }

    [QueryString("q")]
    public string? Query { get; set; }
}

public class SearchForAuthorsResponse
{
    [JsonPropertyName("authors")]
    public string[] Authors { get; set; } = [];
}

public class AssignIssueRequest
{
    [QueryString("assignee")]
    public string? Assignee { get; set; }

    [QueryString("issue")]
    public required string IssueKey { get; set; }
}

public class AssignIssueResponse
{
    [JsonPropertyName("issue")]
    public required Issue Issue { get; set; }

    [JsonPropertyName("components")]
    public Component[] Components { get; set; } = [];

    [JsonPropertyName("rules")]
    public Rule[] Rules { get; set; } = [];

    [JsonPropertyName("users")]
    public User[] Users { get; set; } = [];
}

public class AddCommentRequest
{
    [QueryString("isFeedback")]
    public bool? IsFeedback { get; set; }

    [QueryString("issue")]
    public required string IssueKey { get; set; }

    [QueryString("text")]
    public required string Text { get; set; }
}

public class AddCommentResponse
{
    [JsonPropertyName("issue")]
    public required Issue Issue { get; set; }

    [JsonPropertyName("components")]
    public Component[] Components { get; set; } = [];

    [JsonPropertyName("rules")]
    public Rule[] Rules { get; set; } = [];

    [JsonPropertyName("users")]
    public User[] Users { get; set; } = [];
}

internal class IssuesApi(SonarCloudApiClient client) : IIssuesApi
{
    private const string endpoint = "api/issues";
    public Task<AddCommentResponse> AddComment(AddCommentRequest request, CancellationToken cancellationToken = default)
        => client.Post<AddCommentRequest, AddCommentResponse>(endpoint + "/add_comments", request, cancellationToken);

    public Task<AssignIssueResponse> AssignIssue(AssignIssueRequest request, CancellationToken cancellationToken = default)
        => client.Post<AssignIssueRequest, AssignIssueResponse>(endpoint + "/assign", request, cancellationToken);

    public Task<SearchForAuthorsResponse> SearchForAuthors(SearchForAuthorsRequest request, CancellationToken cancellationToken = default)
        => client.Post<SearchForAuthorsRequest, SearchForAuthorsResponse>(endpoint + "/authors", request, cancellationToken);

    public Task<BulkChangeResponse> BulkChange(BulkChangeRequest request, CancellationToken cancellationToken = default)
        => client.Post<BulkChangeRequest, BulkChangeResponse>(endpoint + "/bulk_change", request, cancellationToken);

    public Task<DeleteCommentResponse> DeleteComment(DeleteCommentRequest request, CancellationToken cancellationToken = default)
        => client.Post<DeleteCommentRequest, DeleteCommentResponse>(endpoint + "/delete_comment", request, cancellationToken);

    public Task<DoTransitionResponse> DoTransition(DoTransitionRequest request, CancellationToken cancellationToken = default)
        => client.Post<DoTransitionRequest, DoTransitionResponse>(endpoint + "/do_transition", request, cancellationToken);

    public Task<EditCommentResponse> EditComment(EditCommentRequest request, CancellationToken cancellationToken = default)
        => client.Post<EditCommentRequest, EditCommentResponse>(endpoint + "/edit_comment", request, cancellationToken);

    public Task<GetChangelogResponse> GetChangelog(GetChangelogRequest request, CancellationToken cancellationToken = default)
            => client.Get<GetChangelogRequest, GetChangelogResponse>(endpoint + "/changelog", request, cancellationToken);

    public Task<GetTagsResponse> GetTags(GetTagsRequest request, CancellationToken cancellationToken = default)
        => client.Get<GetTagsRequest, GetTagsResponse>(endpoint + "/tags", request, cancellationToken);

    public Task<SearchForIssuesResponse> SearchForIssues(SearchForIssuesRequest request, CancellationToken cancellationToken = default)
        => client.Get<SearchForIssuesRequest, SearchForIssuesResponse>(endpoint + "/search", request, cancellationToken);

    public Task<SetTagsResponse> SetTags(SetTagsRequest request, CancellationToken cancellationToken = default)
        => client.Post<SetTagsRequest, SetTagsResponse>(endpoint + "/set_tags", request, cancellationToken);
}
