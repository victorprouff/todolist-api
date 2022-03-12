using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Todolist.Api.Bases.ExceptionHandling;
using Todolist.Api.Bases.ExceptionHandling.Filters;
using Todolist.Api.Filters.ExceptionFilters;

namespace Todolist.Api.Extensions;

public static class ExceptionHandlingExtensions
{
    public static IServiceCollection AddErrorOptions(this IServiceCollection service, IConfiguration configuration) => service
        .AddErrorOptions(configuration, "ErrorInformation")
        .Configure<FilterCollection>(
            filterCollection =>
            {
                foreach (var exceptionFilterType in new[] { typeof(ApiExceptionFilter) })
                {
                    filterCollection.Add(exceptionFilterType);
                }

                var errorDetails = typeof(ErrorDetails);
                foreach (var statusCode in new[] { 500, 400 })
                {
                    filterCollection.Add(new ProducesResponseTypeAttribute(errorDetails, statusCode));
                }
            });

    public static IMvcBuilder AddErrorFilterHandling(this IMvcBuilder builder)
    {
        ErrorCode.AddErrorCodes(ApiErrorCode.ApiErrorCodes);

        return builder.AddErrorFilterHandling(
            new[] { typeof(ApiExceptionFilter)},
            new[] { 500, 400 });
    }
}