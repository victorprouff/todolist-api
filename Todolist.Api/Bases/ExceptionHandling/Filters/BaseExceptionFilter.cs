using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Serilog.Context;

namespace Todolist.Api.Bases.ExceptionHandling.Filters;

public abstract class BaseExceptionFilter<T> : IExceptionFilter
    where T : IExceptionFilter
{
    protected readonly ILogger<T> Logger;
    private readonly ErrorInformationOptions errorInformations;

    protected BaseExceptionFilter(ILogger<T> logger, IOptions<ErrorInformationOptions> informationOptions)
    {
        Logger = logger;
        errorInformations = informationOptions.Value;
    }

    public void OnException(ExceptionContext context)
    {
        if (UseFilter(context.Exception) == false)
        {
            return;
        }

        context.HttpContext.Response.StatusCode = (int)GetStatusCode(context.Exception);
        var errorDetails = BuildErrorDetails(context);
        using (LogContext.PushProperty("ExceptionType", context.Exception.GetType().Name))
        {
            LogExceptionByExceptionType(context, errorDetails);
        }

        context.Result = new JsonResult(errorDetails);
    }

    protected abstract bool UseFilter(Exception exception);

    protected virtual HttpStatusCode GetStatusCode(Exception exception) => HttpStatusCode.InternalServerError;

    protected virtual ErrorDetails BuildErrorDetails(ExceptionContext context) => CreateErrorDetails(context, ErrorCode.Get(ErrorCode.NoErrorCode));

    protected virtual void LogExceptionByExceptionType(ExceptionContext context, ErrorDetails errorDetails) => LogError(
        context,
        errorDetails,
        "Unhandled {ExceptionName}, {BaseExceptionName} on call {EndpointUrl}",
        errorDetails.Name,
        context.Exception.GetType().BaseType?.Name ?? "Exception",
        context.HttpContext.Request.Path);

    protected void LogError(ExceptionContext context, ErrorDetails errorDetails, string message, params object[] args)
    {
        using (LogContext.PushProperty("ErrorResponse", errorDetails, true))
        using (LogContext.PushProperty("ExceptionName", errorDetails.Name))
        using (LogContext.PushProperty("EndpointUrl", context.HttpContext.Request.Path))
        {
            Logger.LogError(context.Exception, message, args);
        }
    }

    protected void LogWarning(ExceptionContext context, ErrorDetails errorDetails)
    {
        using (LogContext.PushProperty("ErrorResponse", errorDetails, true))
        using (LogContext.PushProperty("ExceptionName", errorDetails.Name))
        using (LogContext.PushProperty("EndpointUrl", context.HttpContext.Request.Path))
        {
            Logger.LogWarning(context.Exception, context.Exception.Message);
        }
    }

    protected void LogCritical(ExceptionContext context, ErrorDetails errorDetails)
    {
        using (LogContext.PushProperty("ErrorResponse", errorDetails, true))
        using (LogContext.PushProperty("ExceptionName", errorDetails.Name))
        using (LogContext.PushProperty("EndpointUrl", context.HttpContext.Request.Path))
        {
            Logger.LogCritical(context.Exception, context.Exception.Message);
        }
    }

    protected void LogInformation(ExceptionContext context, ErrorDetails errorDetails)
    {
        using (LogContext.PushProperty("ErrorResponse", errorDetails, true))
        using (LogContext.PushProperty("ExceptionName", errorDetails.Name))
        using (LogContext.PushProperty("EndpointUrl", context.HttpContext.Request.Path))
        {
            Logger.LogInformation(context.Exception, context.Exception.Message);
        }
    }

    protected ErrorDetails CreateErrorDetails(ExceptionContext context, ErrorCode code) => new ErrorDetails(
        context.Exception.GetBaseException().Message,
        context.HttpContext.Response.StatusCode.ToString(),
        context.Exception.GetType().Name,
        code,
        context.HttpContext.TraceIdentifier,
        Activity.Current?.Id,
        new Dictionary<string, object>
        {
            { code.Code, new[] { context.Exception.GetBaseException().Message } }
        },
        errorInformations.ShowStackTrace ? context.Exception.StackTrace : null);
}