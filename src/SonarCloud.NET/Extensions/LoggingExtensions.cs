using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace SonarCloud.NET.Extensions;
internal static partial class LoggingExtensions
{
    [LoggerMessage(
        EventId = 0,
        Level = LogLevel.Critical,
        Message = "Recieved ErrorCode: `{StatusCode}` - '{Message}'"
    )]
    public static partial void HttpError(this ILogger logger, HttpStatusCode statusCode, string message);

    [LoggerMessage(
        EventId = 1,
        Level = LogLevel.Critical,
        Message = "Fout bij deserializeren van response: '{Response}'"
    )]
    public static partial void DeserializationError(this ILogger logger, string response);


    public static void UnknownError(this ILogger logger, HttpStatusCode statusCode)
        => HttpError(logger, statusCode, "Unhandled httpError error.");
}
