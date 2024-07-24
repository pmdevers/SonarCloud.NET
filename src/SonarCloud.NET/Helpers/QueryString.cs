using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using System.Web;

namespace SonarCloud.NET.Helpers;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public class QueryStringAttribute(string name) : Attribute
{
    public string Name { get; set; } = name;
}

internal static class QueryString
{
    public static string ToQueryString<T>(T request)
    {
        var props = typeof(T).GetProperties();
        var collection = new NameValueCollection();
        foreach (var property in props)
        {
            var attrib = property.GetCustomAttribute<QueryStringAttribute>();
            if(attrib is null)
                continue;

            var value = property.GetValue(request)?.ToString() ?? string.Empty;
            collection.Add(attrib.Name, value);
        }

        return ToQueryString(collection);
    }


    [SuppressMessage("Critical Code Smell", "S3776:Cognitive Complexity of methods should not be too high",
        Justification = "<Pending>")]
    public static string ToQueryString(this NameValueCollection collection)
    {
        if (!collection.HasKeys())
            return string.Empty;

        var sb = new StringBuilder();

        for (var i = 0; i < collection.Count; i++)
        {
            string text = collection.GetKey(i) ??string.Empty;
            
                text = HttpUtility.UrlEncode(text) ?? string.Empty;

                string val = (text != null) ? (text + "=") : string.Empty;
                var vals = collection.GetValues(i);

                if (sb.Length > 0)
                    sb.Append('&');

                if (vals == null || vals.Length == 0)
                    sb.Append(val);
                else
                {
                    if (vals.Length == 1)
                    {
                        sb.Append(val);
                        sb.Append(HttpUtility.UrlEncode(vals[0]));
                    }
                    else
                    {
                        for (var j = 0; j < vals.Length; j++)
                        {
                            if (j > 0)
                                sb.Append('&');

                            sb.Append(val);
                            sb.Append(HttpUtility.UrlEncode(vals[j]));
                        }
                    }
                }
            }

        return "?" + sb.ToString().TrimStart('&');
    }
}
