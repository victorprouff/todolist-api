using Microsoft.AspNetCore.Mvc;

namespace Todolist.Api.Extensions;

public static class VersioningExtensions
{
    public static IServiceCollection AddVersioning(this IServiceCollection services, int major, int minor) => services
        .AddApiVersioning(
            options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(major, minor);
            })
        .AddVersionedApiExplorer(
            options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });
}