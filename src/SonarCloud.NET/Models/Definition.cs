namespace SonarCloud.NET.Models;

public record Definition(string Key, string Name, string Description, string Type, string Category, string SubCategory, bool MultiValues, string DefaultValue, string[] Options, Definition[] Fields);
