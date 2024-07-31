using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SonarCloud.NET.Models;
public class Duplication
{
    [JsonPropertyName("blocks")]
    public Block[] Blocks { get;set; } = [];

    public class Block
    {
        [JsonPropertyName("from")]
        public int From { get; set; }
        [JsonPropertyName("size")]
        public int Size { get; set; }
        [JsonPropertyName("_ref")]
        public string Reference { get; set; } = string.Empty;
    }
}
