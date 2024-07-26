using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SonarCloud.NET.Helpers;
internal class JsonDateTimeParseConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var culture = CultureInfo.CreateSpecificCulture("en-US");
        return DateTime.Parse(reader.GetString() ?? string.Empty, culture);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
