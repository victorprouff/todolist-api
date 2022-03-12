using System.Diagnostics;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Todolist.Api.Bases.ExceptionHandling.Filters;

namespace Todolist.Api.Bases.ExceptionHandling;

public static class ExceptionHandlingExtensions
{
    public static IServiceCollection AddErrorOptions(this IServiceCollection service, IConfiguration configuration, string configurationKey = "ErrorInformation") =>
        service.Configure<ErrorInformationOptions>(configuration.GetSection(configurationKey));

    /**
         * <summary>AddErrorFilterHandling.</summary>
         * <param name="builder">description <see cref="IMvcBuilder" />. </param>
         * <param name="filters"> Dictionary of exception filter with list of StatusCode.</param>
         * ∂
         * <param name="statusCodes"> Status code of all possible exceptions. </param>
         * ∂
         * <returns><see cref="IMvcBuilder" />.</returns>
         */
    public static IMvcBuilder AddErrorFilterHandling(this IMvcBuilder builder, IEnumerable<Type> filters, IEnumerable<int> statusCodes) => builder
        .AddMvcOptions(o =>
        {
            foreach (var exceptionFilterType in filters)
            {
                o.Filters.Add(exceptionFilterType);
            }

            var errorDetails = typeof(ErrorDetails);
            foreach (var statusCode in statusCodes)
            {
                o.Filters.Add(new ProducesResponseTypeAttribute(errorDetails, statusCode));
            }
        });
}