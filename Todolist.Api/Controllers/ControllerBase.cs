using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Todolist.Api.Bases.ExceptionHandling.Filters;
using Todolist.Api.Filters.ExceptionFilters;

namespace Todolist.Api.Controllers;

public class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
{
    protected ObjectResult Forbidden()
    {
        var error = ErrorCode.Get(ApiErrorCode.Forbidden);

        return StatusCode(
            StatusCodes.Status403Forbidden,
            new ErrorDetails(
                error.Label ?? string.Empty,
                StatusCodes.Status403Forbidden.ToString(),
                error.Code,
                error,
                HttpContext.TraceIdentifier,
                Activity.Current?.Id));
    }

    protected new NotFoundObjectResult NotFound()
    {
        var error = ErrorCode.Get(ApiErrorCode.NotFound);

        return NotFound(new ErrorDetails(
            error.Label ?? string.Empty,
            StatusCodes.Status404NotFound.ToString(),
            error.Code,
            error,
            HttpContext.TraceIdentifier,
            Activity.Current?.Id));
    }
}