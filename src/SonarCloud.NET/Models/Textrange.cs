using System.Text.Json.Serialization;

namespace SonarCloud.NET.Models;

public class Textrange
{
    [JsonPropertyName("startLine")]
    public int StartLine { get; set; }
    [JsonPropertyName("endLine")]
    public int EndLine { get; set; }
    [JsonPropertyName("startOffset")]
    public int StartOffset { get; set; }
    [JsonPropertyName("endOffset")]
    public int EndOffset { get; set; }
}
