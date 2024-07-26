using System.Reflection;
using System.Web;

namespace SonarCloud.NET.Helpers;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public class QueryStringAttribute(string name) : Attribute
{
    public string Name { get; set; } = name;
}

internal static class QueryString
{
    public static IDictionary<string, string> ToNameValueCollection<T>(T request)
    {
        var props = typeof(T).GetProperties();
        var collection = new Dictionary<string, string>();
        foreach (var property in props)
        {
            var attrib = property.GetCustomAttribute<QueryStringAttribute>();
            if (attrib is null)
                continue;

            var value = property.GetValue(request);
            if (value != null)
            {
                var stringValue = value.ToString();
                if (!string.IsNullOrEmpty(stringValue))
                {
                    collection.Add(attrib.Name, stringValue);
                }
            }
        }

        return collection;
    }

    public static string AsQueryString<T>(T request)
    {
        var collection = ToNameValueCollection(request);
        return ToQueryString(collection);
    }

    private static string ToQueryString(IDictionary<string, string> parameters)
     => parameters.Any() 
        ? "?" + string.Join("&",
            parameters.Select(kvp =>
                string.Format("{0}={1}", kvp.Key, HttpUtility.UrlEncode(kvp.Value)))) 
        : string.Empty;
}
