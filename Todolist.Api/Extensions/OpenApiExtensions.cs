using System.Reflection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using NodaTime;
using Swashbuckle.AspNetCore.SwaggerGen;
using Todolist.Api.Bases.ExceptionHandling.Filters;

namespace Todolist.Api.Extensions;

public static class OpenApiExtensions
{
    public static IServiceCollection AddOpenApi(this IServiceCollection services) => services
        .AddEndpointsApiExplorer()
        .AddSwagger(
            new Dictionary<string, OpenApiInfo> { { "v1", new OpenApiInfo { Title = "Todolist Api", Version = "1" } } },
            MapOption,
            Path.Combine(AppContext.BaseDirectory, typeof(OpenApiExtensions).GetTypeInfo().Assembly.GetName().Name + ".xml"));

    public static IApplicationBuilder UseVersionedOpenApi(this WebApplication app) =>
        app.UseVersionedSwagger(new[] { "v1" }, app.Configuration.GetValue<string>("EnvironmentType") == "CI");

    private static SwaggerGenOptions MapOption(SwaggerGenOptions options)
    {
        options.MapType<Instant>(
            () => new OpenApiSchema { Type = "string", Example = new OpenApiString(Instant.FromDateTimeUtc(DateTime.UtcNow).ToString()), Format = "date-time" });
        options.MapType<Instant?>(
            () => new OpenApiSchema { Type = "string", Example = new OpenApiString(Instant.FromDateTimeUtc(DateTime.UtcNow).ToString()), Format = "date-time" });
        options.MapType<LocalDate>(() => new OpenApiSchema { Type = "string", Example = new OpenApiString("2020-06-20"), Format = "date" });
        options.MapType<LocalDate?>(() => new OpenApiSchema { Type = "string", Example = new OpenApiString("2020-06-20"), Format = "date" });

        options.MapType<ErrorCode>(
            () => new OpenApiSchema
            {
                Type = "string",
                Enum = ErrorCode.GetValues()
                    .Select(errorCode => (IOpenApiAny)new OpenApiString(errorCode.Code))
                    .ToList()
            });

        return options;
    }
}