using System.Net;
using Microsoft.Extensions.Options;
using Todolist.Api.Bases.ExceptionHandling;
using Todolist.Api.Bases.ExceptionHandling.Filters;
using Todolist.Api.Exceptions;

namespace Todolist.Api.Filters.ExceptionFilters;

public class ApiExceptionFilter : BaseExceptionFilter<ApiExceptionFilter>
{
    public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger, IOptions<ErrorInformationOptions> informationOptions)
        : base(logger, informationOptions)
    {
    }

    // Liste les exceptions qui ne sont pas concerné par ce filtre.
    protected override bool UseFilter(Exception exception) => exception is ApiException;

    protected override HttpStatusCode GetStatusCode(Exception exception) => exception switch
    {
        // Map les exceptions ver le bon code erreur
        BadHttpRequestException => HttpStatusCode.BadRequest,
        BadImageFormatException => HttpStatusCode.BadRequest,
        _ => HttpStatusCode.BadRequest
    };
}