using Todolist.Api.Bases.ExceptionHandling.Filters;

namespace Todolist.Api.Filters.ExceptionFilters;

public static class ApiErrorCode
{
    public const string NotFound = "NotFound";
    public const string Forbidden = "Forbidden";
    public const string BadRequest = "BadRequest";

    public static readonly List<ErrorCode> ApiErrorCodes = new()
    {
        new ErrorCode(NotFound, "The resource was not found"),
        new ErrorCode(Forbidden, "The server understood the request but refuses to authorize it"),
        new ErrorCode(BadRequest, "The request was invalid")
    };
}