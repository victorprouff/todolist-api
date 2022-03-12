using System.Text.Json.Serialization;

namespace Todolist.Api.Bases.ExceptionHandling.Filters;

public class ErrorDetails
{
    private const string DefaultTitle = "One or more validation errors occurred.";

    public ErrorDetails(
        string? message,
        string statusCode,
        string name,
        ErrorCode errorCode,
        string requestId,
        string? traceId,
        Dictionary<string, object>? errors = null,
        string? stackTrace = null)
    {
        Message = message ?? DefaultTitle;
        StatusCode = statusCode;
        Name = name;
        ErrorCode = errorCode;
        RequestId = requestId;
        TraceId = traceId;
        Errors = errors ?? new Dictionary<string, object>();
        StackTrace = stackTrace;
    }

    public string StatusCode { get; }

    [JsonConverter(typeof(ErrorCodeJsonConverter))]
    public ErrorCode ErrorCode { get; }

    public string Name { get; }
    public string Message { get; }
    public string RequestId { get; }
    public string? TraceId { get; }
    public string? StackTrace { get; }
    public Dictionary<string, object> Errors { get; }
}