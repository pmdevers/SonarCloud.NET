using System.Text.Json.Serialization;

namespace SonarCloud.NET.Models;
public class Paged<T>
{
    [JsonPropertyName("paging")]
    public Paging Paging { get; set; } = new();
    [JsonPropertyName("components")]
    public List<T> Components { get; set; } = []; 
}

public class Paging
{
    [JsonPropertyName("pageIndex")]
    public int Index { get;set; } = 0;
    [JsonPropertyName("pageSize")]
    public int Size { get;set; } = 0;
    [JsonPropertyName("total")] 
    public int Total { get;set;} = 0;
}
