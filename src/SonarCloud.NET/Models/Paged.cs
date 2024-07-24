using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SonarCloud.NET.Models;
public class Paged<T>
{
    public Paging Paging { get; set; } = new();
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
